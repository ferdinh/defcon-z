using DefconZ.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Units.Special
{
    public class PrecisionBomb : SpecialAbility
    {
        public float distanceFromTarget;
        public float height;
        public float planeSpeed;
        public float bombSpeed;
        public float bombDamage;
        public static float abilityCost = 500f;

        public GameObject plane;
        public GameObject bomb;

        private Vector3 target;
        public Vector3 planeTarget;
        public Vector3 planeFinalTarget;

        private MeshRenderer bombMesh;
        private SphereCollider bombCollider;
        private AudioSource audioSource;

        private Vector3 rotation;
        
        private bool bombDropped = false;
        private bool exploded = false;
        private bool disabled = false;

        public AbilityType type = AbilityType.PrecisionBomb;

        /// <summary>
        /// Starts the ability
        /// </summary>
        /// <param name="target"></param>
        /// <param name="eulerAngle"></param>
        /// <param name="cam"></param>
        public void StartAbility(Vector3 target, Vector3 eulerAngle, GameObject cam)
        {
            abilityName = "Air Strike";
            audioSource = GetComponentInChildren<AudioSource>();
            bombMesh = bomb.GetComponentInChildren<MeshRenderer>();
            bombCollider = bomb.GetComponent<SphereCollider>();
            bomb.GetComponent<BombCollider>().bombDamage = bombDamage;

            this.target = target;
            planeTarget = target;
            planeTarget.y = height;

            Vector3 pos = new Vector3
            {
                y = height,
                x = cam.transform.position.x + (distanceFromTarget * Mathf.Sin(cam.transform.rotation.eulerAngles.y * Mathf.Deg2Rad)),
                z = cam.transform.position.z + (distanceFromTarget * Mathf.Cos(cam.transform.rotation.eulerAngles.y * Mathf.Deg2Rad))
            };

            planeFinalTarget = pos * -1;
            planeFinalTarget.y = height;

            gameObject.transform.position = pos;

            // Set the rotation of the plane
            rotation = new Vector3
            {
                y = cam.transform.rotation.eulerAngles.y + 180.0f
            };

            gameObject.transform.eulerAngles = rotation;
        }

        /// <summary>
        /// Moves the objects towards their respective targets
        /// </summary>
        private void MoveTowardsTarget()
        {
            if (!bombDropped)
            {
                // Move towards the drop point
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, planeTarget, planeSpeed * Time.deltaTime);
            }
            else
            {
                // Move the bomb and the plane
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, planeFinalTarget, planeSpeed * Time.deltaTime);
                bomb.transform.position = Vector3.MoveTowards(bomb.transform.position, target, bombSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// Drops the bomb
        /// </summary>
        public void DropBomb()
        {
            Debug.Log("Dropping bomb!");
            bombDropped = true;
            bomb.transform.parent = null;
        }

        /// <summary>
        /// Explodes the bomb
        /// </summary>
        public void ExplodeBomb()
        {
            exploded = true;
            bombMesh.enabled = false;
            bombCollider.enabled = true;
            audioSource.Play();
        }

        /// <summary>
        /// Cleans up the ability
        /// </summary>
        private void CleanUp()
        {
            Destroy(bomb, audioSource.clip.length);
            Destroy(this.gameObject, audioSource.clip.length);
        }

        public void Update()
        {
            // If the bomb has exploded at the start of the update, disable the collider
            if (exploded && !disabled)
            {
                bombCollider.enabled = false;
                disabled = true;

                CleanUp();
            }

            MoveTowardsTarget();

            if (!bombDropped && Vector3.Distance(gameObject.transform.position, target) <= height + 10.0f)
            {
                DropBomb();
            }

            if (bombDropped && !exploded && Vector3.Distance(bomb.transform.position, target) <= 0.5f)
            {
                ExplodeBomb();
            }
        }
    }
}