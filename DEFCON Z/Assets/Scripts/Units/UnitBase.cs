using DefconZ.Entity;
using DefconZ.Entity.Action;
using DefconZ.GameLevel;
using DefconZ.Simulation;
using DefconZ.Units;
using DefconZ.Units.Actions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefconZ
{
    public abstract class UnitBase : ObjectBase, IDestructible
    {
        public float health = 100;
        public float maxHealth = 100;
        public float attackRange = 1;
        public float attackTime = 1;
        public float sightRange = 15;
        public Faction FactionOwner { get; set; }
        public Faction enemyFaction;
        public Zone currentZone;
        public AudioClip deathSound;
        public AudioClip attackSound;
        public UnitAttackScript unitAttackScript;
        public float RecruitCost;
        public float Upkeep;
        public int RecruitTime;
        public int XP;

        public GameObject unitModel;

        [SerializeField]
        public AudioSource audioSource;

        private GameManager _gameManager;

        /*
         * All unit has an initial base health of 100 and base damage of 10.
         * The most barebone/basic unit takes 10x hit to be defeated.
         *
         */
        protected float baseHealth = 100.0f;
        [SerializeField]
        protected float baseDamage = 10.0f;
        public IList<Modifier> Modifiers = new List<Modifier>();

        // Start is called before the first frame update
        public void Awake()
        {
            _gameManager = GameObject.FindGameObjectWithTag(nameof(GameManager)).GetComponent<GameManager>();
            unitAttackScript = GetComponent<UnitAttackScript>();
            audioSource = GetComponent<AudioSource>();

            InitUnit();
        }
        public void Start()
        {
            foreach (Faction f in _gameManager.Factions)
            {
                if (FactionOwner != f)
                {
                    enemyFaction = f;
                    unitAttackScript.enemyFaction = f;
                }
            }
        }

        /// <summary>
        /// Initialises the unit, called upon the first frame the unit is instantiated
        /// </summary>
        public abstract void InitUnit();

        /// <summary>
        /// Defines how a unit is destroyed
        /// </summary>
        public virtual void DestroySelf()
        {
            Debug.Log(this.objName + " has reached 0 or less health and has died");

            // Ask the unit to remove itself the current zone
            if (currentZone != null)
            {
                currentZone.RemoveFromZone(this);
            }

            // If the unit has a compbat, it should stop
            Destroy(unitAttackScript);

            // If the unit is moving, it should stop
            UnitMoveScript moveScript = GetComponent<UnitMoveScript>();
            moveScript.StopMoving();
            Destroy(moveScript);

            // Remove the unit from the faction list
            FactionOwner.Units.Remove(this.gameObject);

            // Rotate the model
            unitModel.transform.eulerAngles = new Vector3(90, unitModel.transform.rotation.y, unitModel.transform.rotation.z);

            // set the audio source clip to the death sound clip
            audioSource.clip = deathSound;
            audioSource.Play();

            _gameManager.RemoveUnit(this, deathSound.length);
        }

        /// <summary>
        /// Defines how a unit takes damage
        /// If the remaining health is less than or equal to zero, the unit is destroyed
        /// </summary>
        /// <param name="damage"></param>
        public virtual void TakeDamage(float damage)
        {
            Debug.Log(this.objName + " " + this.health);

            health -= damage;

            // When a unit dies, remove the combat from list of combats and
            // remove the combat from the winning unit whilst destroy the
            // losing unit.
            if (!IsAlive())
            {
                DestroySelf();
            }
        }

        /// <summary>
        /// Takes the damage from another unit.
        /// </summary>
        /// <param name="hostileUnit">The hostile unit.</param>
        public virtual void TakeDamageFrom(UnitBase hostileUnit)
        {
            var damageToTake = hostileUnit.CalculateDamage();

            health -= damageToTake;

            if (!IsAlive())
            {
                // Hostile unit faction will be awarded with XP corresponding
                // to how hard this unit is to defeat.
                hostileUnit.FactionOwner.Level.AddXP(XP);

                DestroySelf();
            }
        }

        /// <summary>
        /// Determines whether this unit is alive.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this unit is alive; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAlive()
        {
            return health > 0;
        }

        /// <summary>
        /// Calculates the damage to be inflicted.
        /// </summary>
        /// <returns>Damage given.</returns>
        public float CalculateDamage()
        {
            // To adjust different value in a unit,
            // add more modifier to derived classes
            float baseModifier = 1.0f;
            float attackModifier = baseModifier + Modifiers.Sum(mod => mod.Value);

            float variableMultiplier = UnityEngine.Random.Range(0.05f, 0.1f);

            return baseDamage * (attackModifier + variableMultiplier);
        }

        /// <summary>
        /// Checks if this instance currently have a combat.
        /// </summary>
        /// <returns>True if combat is present, else, false.</returns>
        public bool CombatPresent()
        {
            return unitAttackScript.InCombat();
        }

        ///<summary>
        /// Starts an attack for the unit
        /// </summary>
        public void StartAttack(GameObject target)
        {
            GetComponent<UnitAttackScript>().StartAttack(target);
        }

        /// <summary>
        /// Plays the attack sound.
        /// </summary>
        public void PlayAttackSound()
        {
            if (audioSource != null)
            {
                audioSource.clip = attackSound;
                audioSource.Play();
            }
        }
    }
}