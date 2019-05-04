using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public class CameraController : MonoBehaviour
    {
        public GameObject camObject;
        public Camera mainCamera;

        [Header("Camera Movement")]
        public float minSpeed;
        public float maxSpeed;
        public float minHeight;
        public float maxHeight;

        [Header("Camera Rotation")]
        public float rotationSpeed;
        public float minRotX;
        public float maxRotX;

        [Header("Other Settings")]
        public float boostMultiplier;
        public bool boost;

        private void Awake()
        {
            mainCamera = camObject.GetComponentInChildren<Camera>();
            boost = false;
        }

        // Update is called once per frame
        void Update()
        {
            // set the current state of the camera speed boost
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                boost = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                boost = false;
            }

            MoveCamera(); // calculate and apply camera movement
            RotateCamera(); // calculate and apply camera rotation
        }

        /// <summary>
        /// Rotates the attached camera based on user input
        /// </summary>
        private void RotateCamera()
        {

            if (Input.GetAxis("CameraRotation") != 0)
            {
                Vector3 rotation = new Vector3(0, 0, 0);

                rotation.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

                rotation.x += -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime; // inverted value for rotation, negative rotation is "up"

                rotation.x = Mathf.Clamp(rotation.x, minRotX, maxRotX);

                if (rotation.y >= 360.0f || rotation.y <= -360.0f)
                {
                    rotation.y = 0.0f;
                }

                camObject.transform.eulerAngles = camObject.transform.eulerAngles + rotation;
            }
        }

        /// <summary>
        /// Moves the attached camera based on user input
        /// </summary>
        private void MoveCamera()
        {
            Vector3 _target = Vector3.zero;
            float _intialY = camObject.transform.position.y;

            if (Input.GetAxis("Vertical") != 0)
            {
                _target += camObject.transform.forward * (minSpeed * Input.GetAxis("Vertical")) * Time.deltaTime;
            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                _target += camObject.transform.right * (minSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime;
            }

            _target.y = 0; // reset the y transformation to 0 so that panning does not effect height

            if (Input.GetAxis("YMovementAxis") != 0)
            {
                _target.y += minSpeed * Input.GetAxis("YMovementAxis") * Time.deltaTime;
            }

            if (boost)
            {
                _target *= boostMultiplier;
            }

            camObject.transform.position += _target;
        }
    }
}

