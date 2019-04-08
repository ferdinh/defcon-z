using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Camera Movement")]
	public float minSpeed;
	public float maxSpeed;
	public float minHeight;
	public float maxHeight;

    [Header("Camera Rotation")]
    public float rotationSpeed;
	public float minRotX;
	public float maxRotX;

    [Header("Other Settings")]
    public float boostMultiplier;
    public bool boost;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        boost = false;
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
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            target += cam.transform.right * (minSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime;
        }
        if (Input.GetAxis("YMovementAxis") != 0)
        {
			target.y = 0;
			target.y += minSpeed * Input.GetAxis("YMovementAxis") * Time.deltaTime;
        }

        
        cam.transform.position += target;
    }
}
