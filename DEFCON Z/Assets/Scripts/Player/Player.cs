using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public class Player : MonoBehaviour
    {
        public CameraController cam;
        // for testing purposes, show in editor
        [SerializeField]
        private GameObject selectedObject;

        // Start is called before the first frame update
        void Start()
        {
            cam = gameObject.GetComponent<CameraController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// RayCasts to the first object the rayhit returns and if the object is selectable, the object is selected
        /// </summary>
        public void SelectObject()
        {
            RaycastHit _rayCastHit = new RaycastHit();

            bool _selectable = false;

            // check if the raycast hit anything
            if (Physics.Raycast(cam.mainCamera.ScreenPointToRay(Input.mousePosition), out _rayCastHit))
            {
                Debug.Log("Hit: " + _rayCastHit.transform.name);
                // check if the object hit is tagged as a game object
                if (_rayCastHit.transform.gameObject.tag == "GameObject")
                {
                    _selectable = true;
                    selectedObject = _rayCastHit.transform.gameObject;
                }
            }
            // if player has not clicked on a selectable object, make sure the currently selected object is cleared
            if (!_selectable)
            {
                selectedObject = null;
            }
        }

        public void SelectedObjectAction()
        {
            //UnitBase _selectedUnit;

            if (selectedObject != null)
            {
                UnitBase _selectedUnit = selectedObject.GetComponent<UnitBase>();
                if (_selectedUnit != null)
                {
                    // at this point we know the object can accept an order
                    // raycast for the order location
                    RaycastHit _rayCastHit = new RaycastHit();

                    // check that the player has clicked somewhere
                    if (Physics.Raycast(cam.mainCamera.ScreenPointToRay(Input.mousePosition), out _rayCastHit))
                    {
                        Debug.Log(_rayCastHit.transform.position);
                        _selectedUnit.DoCurrentAction(_rayCastHit.point);
                    } 
                }
            }
        }
    }
}

