using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        RaycastHit rayCastHit = new RaycastHit();
        
        bool selectable = false;

        // check if the raycast hit anything
        if (Physics.Raycast(cam.cam.ScreenPointToRay(Input.mousePosition), out rayCastHit))
        {
            // check if the object hit is tagged as a game object
            if (rayCastHit.transform.gameObject.tag == "GameObject")
            {
                selectable = true;
                selectedObject = rayCastHit.transform.gameObject;
            }
        }
        // if player has not clicked on a selectable object, make sure the currently selected object is cleared
        if (!selectable)
        {
            selectedObject = null;
        }
    }
}
