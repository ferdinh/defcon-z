using UnityEngine;

namespace DefconZ.Units
{
    public class UnitPrefabList : MonoBehaviour
    {
        public static UnitPrefabList Instance = null;

        public GameObject Human;
        public GameObject Zombie;

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}