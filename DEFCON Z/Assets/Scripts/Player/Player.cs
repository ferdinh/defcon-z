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
        // check if the raycast hit anything
        if (Physics.Raycast(cam.cam.ScreenPointToRay(Input.mousePosition), out rayCastHit))
        {
            // check if the object hit is tagged as a game object
            if (rayCastHit.transform.gameObject.tag == "GameObject")
            {
                Debug.Log("Raycast hit: " + rayCastHit.transform.gameObject.GetComponent<ObjectBase>().ObjName + "!");
                selectedObject = rayCastHit.transform.gameObject;
            }
            else
            {
                Debug.Log("Object hit is not a game object!");
                Debug.Log(rayCastHit.transform.gameObject.name);
            }
        } else
        {
            Debug.Log("Raycast did not hit an object!");
        }
    }
}
