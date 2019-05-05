using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public class Rotator : MonoBehaviour
    {
        public float rotationSpeed;
        public float rotation;
        // Start is called before the first frame update
        void Start()
        {
            rotation = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (rotation > 360.0f)
                rotation -= 360.0f;

            gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}