using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minSpeed, maxSpeed;
    public float minHeight, maxHeight;
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
        MoveCamera();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            boost = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            boost = false;
        }
    }

    private void MoveCamera()
    {
        Vector3 movement = new Vector3(0, 0, 0);

        movement.x += Input.GetAxis("Horizontal") * minSpeed * Time.deltaTime;
        movement.z += Input.GetAxis("Vertical") * minSpeed * Time.deltaTime;

        if (boost)
        {
            movement.x *= boostMultiplier;
            movement.z *= boostMultiplier;
        }

        cam.transform.position = new Vector3(cam.transform.position.x + movement.x, cam.transform.position.y, cam.transform.position.z + movement.z);
    }
}
