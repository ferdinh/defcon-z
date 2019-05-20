using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Utility
{
    public class Bouncer : MonoBehaviour
    {
        public float moveAmount;
        public float speed;
        public float bottom;
        public float top;
        private float startHeight;
        // Start is called before the first frame update
        void Start()
        {
            startHeight = gameObject.transform.position.y;
            bottom = startHeight - bottom;
            top = startHeight + top;
            speed *= -1;
        }

        // Update is called once per frame
        void Update()
        {
            float posY = gameObject.transform.position.y;
            if (posY>= top || posY <= bottom)
            {
                speed *= -1;
            }

            gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }
}