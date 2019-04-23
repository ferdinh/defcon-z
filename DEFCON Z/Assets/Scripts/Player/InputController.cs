using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        
        player = gameObject.GetComponent<Player>();
    }

    void Update()
    {
        // Check if the "Boost" key is being pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.cam.boost = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.cam.boost = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            player.SelectObject();
        }
    }
}
