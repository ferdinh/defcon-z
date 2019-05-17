using DefconZ.GameLevel;
using UnityEngine;

namespace DefconZ
{
    public class InputController : MonoBehaviour
    {
        private Player player;
        public ZoneManager zoneManager;

        // Start is called before the first frame update
        private void Start()
        {
            player = gameObject.GetComponent<Player>();
        }

        private void Update()
        {
            // Fire 1 (Left click)
            if (Input.GetButtonDown("Fire1"))
            {
                // Raycast and select an object
                player.SelectObject();
            }

            // Fire 1 (Right Click)
            if (Input.GetButtonDown("Fire2"))
            {
                player.SelectedObjectAction();
            }

            // Check if the "Boost" key is being pressed
            // if the key is pressed, set the boost status to true
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                player.camController.boost = true;
            }
            // if the key is released set the boost status to false
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                player.camController.boost = false;
            }

            // zone display
            if (Input.GetButtonDown("ZoneDisplay"))
            {
                zoneManager.ToggleZoneDisplay();
            }
        }
    }
}