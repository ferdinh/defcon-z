using DefconZ.Entity;
using DefconZ.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefconZ
{
    public class ObjectSelection : MonoBehaviour
    {
        public float selectionBoxDelay;
        public SelectionUI selectionUI;
        public Player player;
        public Camera cam;

        [SerializeField]
        private bool isActive;
        private bool isSelecting;
        private Vector3 initialMousePosition;
        private float timeOfClick; 

        /// <summary>
        /// Performed when the player starts dragging the selection box
        /// </summary>
        public void SelectionStart()
        {
            // Clear the list of selected objects before anything else
            player.selectedObjects.Clear();

            // When first clicked, the player should select an object for single click
            SelectObject();

            // Then initialise fiels incase the player is using a selection box
            isSelecting = true;
            selectionUI.draw = true;
            initialMousePosition = Input.mousePosition;
            selectionUI.initialMousePosition = initialMousePosition;
            timeOfClick = Time.time;
        }

        /// <summary>
        /// Performed when the player stops dragging the selection box
        /// </summary>
        public void SelectionEnd()
        {
            selectionUI.draw = false;

            // Check if the time delay has passed, if the delay has passed, the player is using a selection box
            if (Time.time >= timeOfClick + selectionBoxDelay)
            {
                Debug.Log("Selecting objects using selection box method");
                // Clear the list of any currently selected units
                player.selectedObjects.Clear();

                // Find all selectable gameobjects
                foreach (ObjectBase obj in FindObjectsOfType<ObjectBase>())
                {
                    // Store refernce the othe ObjectBase gameobject
                    GameObject parentObj = obj.gameObject;

                    // Add every selectable object inside the bounds of the selection box to the players selected units
                    if (ObjectInBounds(parentObj))
                    {
                        player.selectedObjects.Add(parentObj);
                    }
                }

                // If we selected any units, perform list filtering
                if (player.selectedObjects.Count > 0)
                {
                    SelectionFiltering();
                }

                player.playerUI.UpdateObjectSelectionUI();
            }

            isSelecting = false;
        }

        /// <summary>
        /// If units and non units are in the list
        /// Selection filtering, removes non units from the list
        /// </summary>
        private void SelectionFiltering()
        {
            bool hasUnits = false;
            bool hasNonUnits = false;

            foreach (GameObject obj in player.selectedObjects)
            {
                UnitBase unit = obj.GetComponent<UnitBase>();

                if (hasUnits == false && unit != null)
                {
                    hasUnits = true;
                }
                else if (hasNonUnits == false && unit == null)
                {
                    hasNonUnits = true;
                }
            }

            if (hasNonUnits && hasUnits)
            {
                // Define a new list of objects to remove, unable to modify a list while accessing it
                List<GameObject> unitsToRemove = new List<GameObject>();

                foreach (GameObject obj in player.selectedObjects)
                {
                    if (obj.GetComponent<UnitBase>() == null)
                    {
                        unitsToRemove.Add(obj);
                    }
                }

                foreach (GameObject obj in unitsToRemove)
                {
                    player.selectedObjects.Remove(obj);
                }
            }
        }

        /// <summary>
        /// Selects a single object
        /// RayCasts to the first object the rayhit returns and if the object is selectable, the object is selected
        /// </summary>
        public void SelectObject()
        {
            bool selectable = false;

            // If they player has an action selected, it should be disabled
            player.selectedAction = false;

            // check if the raycast hit anything
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit rayCastHit))
            {
                Debug.Log("Hit: " + rayCastHit.transform.name);
                // check if the object hit is tagged as a game object
                if (rayCastHit.transform.gameObject.tag == "GameObject")
                {
                    selectable = true;
                    player.selectedObjects.Add(rayCastHit.transform.gameObject);
                }
            }
            // if player has not clicked on a selectable object, make sure the currently selected object is cleared
            if (!selectable)
            {
                player.selectedObjects.Clear();
            }

            player.playerUI.UpdateObjectSelectionUI();
        }

        /// <summary>
        /// Checks whether a gameobject is whithin selection bounds
        /// </summary>
        /// <param name="obj">The gameobject to check</param>
        /// <returns></returns>
        private bool ObjectInBounds(GameObject obj)
        {
            if (!isSelecting)
            {
                return false;
            }

            Bounds bounds = GetScreenViewportBounds(initialMousePosition, Input.mousePosition);

            return bounds.Contains(cam.WorldToViewportPoint(obj.transform.position));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public Bounds GetScreenViewportBounds(Vector3 pos1, Vector3 pos2)
        {
            Vector3 screenPoint1 = cam.ScreenToViewportPoint(pos1);
            Vector3 screenPoint2 = cam.ScreenToViewportPoint(pos2);
            Vector3 screenMin = Vector3.Min(screenPoint1, screenPoint2);
            Vector3 screenMax = Vector3.Max(screenPoint1, screenPoint2);
            screenMin.z = cam.nearClipPlane;
            screenMax.z = cam.farClipPlane;

            Bounds calculatedBounds = new Bounds();
            calculatedBounds.SetMinMax(screenMin, screenMax);

            return calculatedBounds;
        }
    }
}