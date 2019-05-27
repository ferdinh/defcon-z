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

        public GameObject plane;
        public GameObject bomb;


        private Vector3 target;
        public Vector3 planeTarget;

        private MeshRenderer bombMesh;
        private SphereCollider bombCollider;
        
        private bool bombDropped = false;
        private bool initialised = false;
        private bool exploded = false;
        private Vector3 rotation;

        private AudioSource audioSource;

        public void StartAbility(Vector3 target, Vector3 eulerAngle, GameObject cam)
        {
            // Set the position of the plane
            // We want the plane to be the specified distance from the target, behind the player camera
            audioSource = GetComponentInChildren<AudioSource>();
            bombMesh = bomb.GetComponentInChildren<MeshRenderer>();
            bombCollider = bomb.GetComponent<SphereCollider>();
            bomb.GetComponent<BombCollider>().bombDamage = bombDamage;

            this.target = target;
            planeTarget = target;
            planeTarget.y = height;

            Vector3 pos = new Vector3();
            pos.y = height;
            pos.x = cam.transform.position.x + (distanceFromTarget * Mathf.Sin(cam.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
            pos.z = cam.transform.position.z + (distanceFromTarget * Mathf.Cos(cam.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
            gameObject.transform.position = pos;

            // Set the rotation of the plane
            rotation = new Vector3();
            rotation.y = cam.transform.rotation.eulerAngles.y + 180.0f;
            
            gameObject.transform.eulerAngles = rotation;

            initialised = true;
        }

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
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, planeTarget, planeSpeed * Time.deltaTime);
                bomb.transform.position = Vector3.MoveTowards(bomb.transform.position, target, bombSpeed * Time.deltaTime);
            }
        }

        public void DropBomb()
        {
            Debug.Log("Dropping bomb!");
            bombDropped = true;
            bomb.transform.parent = null;
        }

        public void ExplodeBomb()
        {
            exploded = true;
            bombMesh.enabled = false;
            bombCollider.enabled = true;
            audioSource.Play();
        }

        public void Update()
        {
            
                // If the bomb has exploded at the start of the update, disable the collider
                if (exploded)
                {
                    bombCollider.enabled = false;
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