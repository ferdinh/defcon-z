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
        public GameObject indicatorPrefab;

        public Material friendlyMaterial;
        public Material enemyMaterial;

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
                                GameObject orderLocationIndicator = Instantiate(indicatorPrefab, _rayCastHit.point, Quaternion.identity);
                                MeshRenderer orderLocationIndicatorMaterial = orderLocationIndicator.GetComponentInChildren<MeshRenderer>();

                                if (_rayCastHit.transform.gameObject.GetComponent<UnitBase>() != null)
                                {
                                    Debug.Log("Hit another unit");
                                    selectedUnit.StartAttack(_rayCastHit.transform.gameObject);
                                    orderLocationIndicatorMaterial.material = enemyMaterial;
                                }
                                else
                                {
                                    Debug.Log("Clicked move position");
                                    selectedUnit.GetComponent<IMoveable>().MoveTo(_rayCastHit.point);
                                    orderLocationIndicatorMaterial.material = friendlyMaterial;
                                }
                                Destroy(orderLocationIndicator, 4);
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