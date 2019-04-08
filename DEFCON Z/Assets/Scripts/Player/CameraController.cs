using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Movement")]
    public float minSpeed, maxSpeed;
    public float minHeight, maxHeight;

    [Header("Camera Rotation")]
    public float rotationSpeed;
    public float minRotX, maxRotX;

    [Header("Other Settings")]
    public float boostMultiplier;
    public bool boost;

    public Camera cam;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        boost = false;
        pos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // set the current state of the camera speed boost
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            boost = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            boost = false;
        }

        MoveCamera(); // calculate and apply camera movement
        RotateCamera(); // calculate and apply camera rotation
    }

    private void RotateCamera()
    {

        if (Input.GetKey(KeyCode.Mouse2))
        {
            Vector3 rotation = new Vector3(0, 0, 0);

            rotation.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            rotation.x += -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime; // inverted value for rotation, negative rotation is "up"

            rotation.x = Mathf.Clamp(rotation.x, minRotX, maxRotX);

            if (rotation.y >= 360.0f || rotation.y <= -360.0f)
            {
                rotation.y = 0.0f;
            }

            cam.transform.eulerAngles = cam.transform.eulerAngles + rotation;
        }
    }

    private void MoveCamera()
    {
        Vector3 target = Vector3.zero;
        float intialY = cam.transform.position.y;
        

        if (Input.GetAxis("Vertical") != 0)
        {
            target += cam.transform.forward * (minSpeed * Input.GetAxis("Vertical")) * Time.deltaTime;
            target.y = 0;
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            target += cam.transform.right * (minSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime;
            target.y = 0;
        }
        if (Input.GetAxis("YMovementAxis") != 0)
        {
            target.y += minSpeed * Input.GetAxis("YMovementAxis") * Time.deltaTime;
            //cam.transform.position += new Vector3(0, target.y, 0);
        }

        
        cam.transform.position += target;

        //target.transfor *= cam.transform.forward;

        //target.y = 0f;

        //cam.transform.Translate(target, Space.Self);

        //cam.transform.position += cam.transform.forward * target.z;
        //cam.transform.position += cam.transform.right * target.x;

         
        //cam.transform.position += ;

        //cam.transform.position = new Vector3(cam.transform.position.x, 1, cam.transform.position.z);

        //cam.transform.position = new Vector3(cam.transform.position.x + target.x, 0, cam.transform.position.z + target.z);

        // set the target Y position back to it's intial state to prevent camera rotation effecting the vertical position when moving
        //target.y = 0;

        //Debug.Log(target);

        //cam.transform.Translate(target);
        //cam.transform.position = cam.transform.position + target;

        /*
        Vector3 movement = cam.transform.position;

        // calculate velocity, speed is multiplied by deltaTime for smooth movement
        movement.z += Input.GetAxis("Horizontal") * minSpeed * Time.deltaTime;
        movement.x += Input.GetAxis("Vertical") * minSpeed * Time.deltaTime;
        movement.y += Input.GetAxis("YMovementAxis") * minSpeed * Time.deltaTime;

        Debug.Log(Input.GetAxis("Vertical"));

        // if the player is boosting the camera speed, apply the multiplier to the velocity
        if (boost)
        {
            movement *= boostMultiplier;
        }

        //Vector3 target = cam.transform.position + transform.forward * movement.x;
        //target = cam.transform.position + transform.right * movement.z;
        
        
        // Vector3 newCameraPosition = transform.position + transform.forward * walkSpeed * Time.deltaTime;


        cam.transform.position = new Vector3(target.x, transform.position.y, target.z);
        */
    }
}
