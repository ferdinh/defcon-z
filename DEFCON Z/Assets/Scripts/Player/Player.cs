using DefconZ.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public class Player : MonoBehaviour
    {
        public CameraController camController;
        public Camera cam;
        // for testing purposes, show in editor
        [SerializeField]
        public GameObject selectedObject; // Field is public for tests

        public PlayerUI playerUI;

        // Start is called before the first frame update
        void Start()
        {
            camController = GetComponent<CameraController>();
            cam = camController.mainCamera;
        }

        // Update is called once per frame
        void Update() { }

        /// <summary>
        /// RayCasts to the first object the rayhit returns and if the object is selectable, the object is selected
        /// </summary>
        public void SelectObject()
        {
            RaycastHit _rayCastHit = new RaycastHit();

            bool _selectable = false;

            // check if the raycast hit anything
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out _rayCastHit))
            {
                Debug.Log("Hit: " + _rayCastHit.transform.name);
                // check if the object hit is tagged as a game object
                if (_rayCastHit.transform.gameObject.tag == "GameObject")
                {
                    _selectable = true;
                    selectedObject = _rayCastHit.transform.gameObject;
                    playerUI.UpdateObjectSelectionUI(selectedObject.GetComponent<ObjectBase>());
                }
            }
            // if player has not clicked on a selectable object, make sure the currently selected object is cleared
            if (!_selectable)
            {
                selectedObject = null;
                playerUI.UpdateObjectSelectionUI(null);
            }
        }

        public void SelectedObjectAction()
        {
            // Check if the player has selected a unit
            if (selectedObject != null)
            {
                // Check if the selected object is a unit
                UnitBase _selectedUnit = selectedObject.GetComponent<UnitBase>();
                if (_selectedUnit != null)
                {
                    // Check if the selected unit is owned by the player
                    if (_selectedUnit.FactionOwner.IsPlayerUnit)
                    {
                        Debug.Log("Selected unit is a player controlled unit");
                        // at this point we know the object can accept an order
                        // raycast for the order location
                        RaycastHit _rayCastHit = new RaycastHit();

                        // check that the player has clicked somewhere
                        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out _rayCastHit))
                        {
                            if (_rayCastHit.transform.gameObject.GetComponent<UnitBase>() != null)
                            {
                                Debug.Log("Hit another unit");
                                _selectedUnit.StartAttack(_rayCastHit.transform.gameObject);
                            }
                            else
                            {
                                _selectedUnit.MoveTo(_rayCastHit.point);
                                Debug.Log("Clicked move position");
                            }
                        }
                    } else
                    {
                        Debug.Log("Selected unit is not player controlled, cannot give orders");
                    }
                }
            }
        }
    }
}