using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Menu
{
    public class UIManager : MonoBehaviour
    {
        public bool UIActive;
        public KeyCode key;
        public GameObject UIObject;
        // Start is called before the first frame update
        void Start()
        {
            UIActive = false;
            UIObject.SetActive(UIActive);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(key)) {

                UIActive = (UIActive) ? false : true;
                UIObject.SetActive(UIActive);
                
            }
        }
    }
}