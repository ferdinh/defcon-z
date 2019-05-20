using DefconZ.Entity.Action;
using DefconZ.UI;
using DefconZ.Units.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ
{
    public class Player : MonoBehaviour
    {
        public CameraController camController;
        public Camera cam;
        private InGameUI inGameUI;
        public PlayerUI playerUI;
        public ObjectSelection objectSelector;
        public List<GameObject> selectedObjects;

        private void Awake()
        {
            camController = GetComponent<CameraController>();
            cam = camController.mainCamera;

            objectSelector = gameObject.GetComponent<ObjectSelection>();
            objectSelector.cam = cam;

            selectedObjects = new List<GameObject>();
        }

        // Update is called once per frame
        void Update() { }

        public void SelectedObjectAction()
        {
            // Check if the player has selected a unit
            if (selectedObjects.Count > 0)
            {
                // For every object selected, give it an order
                foreach (GameObject obj in selectedObjects)
                {
                    // Check if the selected object is a unit
                    UnitBase selectedUnit = obj.GetComponent<UnitBase>();
                    if (selectedUnit != null)
                    {
                        // Check if the selected unit is owned by the player
                        if (selectedUnit.FactionOwner.IsPlayerUnit)
                        {
                            Debug.Log("Selected unit is a player controlled unit");

                            // check that the player has clicked somewhere
                            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit _rayCastHit))
                            {
                                if (_rayCastHit.transform.gameObject.GetComponent<UnitBase>() != null)
                                {
                                    Debug.Log("Hit another unit");
                                    selectedUnit.StartAttack(_rayCastHit.transform.gameObject);
                                }
                                else
                                {
                                    selectedUnit.GetComponent<IMoveable>().MoveTo(_rayCastHit.point);
                                    Debug.Log("Clicked move position");
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Selected unit is not player controlled, cannot give orders");
                        }
                    }
                }
            }
        }
    }
}