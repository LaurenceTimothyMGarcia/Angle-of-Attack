using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace base_movement
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
            //Right movement
            if (Input.GetKey(KeyCode.D))
            {
                VirtualInputManager.Instance.moveRight = true;
            }
            else
            {
                VirtualInputManager.Instance.moveRight = false;
            }
            //Left movement
            if (Input.GetKey(KeyCode.A))
            {
                VirtualInputManager.Instance.moveLeft = true;
            }
            else
            {
                VirtualInputManager.Instance.moveLeft = false;
            }
            //Run
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                VirtualInputManager.Instance.run = true;
            }
            else
            {
                VirtualInputManager.Instance.run = false;
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.W))
            {
                VirtualInputManager.Instance.jump = true;
            }
            else
            {
                VirtualInputManager.Instance.jump = false;
            }

            //Dash
            if (Input.GetKey(KeyCode.Space))
            {
                VirtualInputManager.Instance.dash = true;
            }
            else
            {
                VirtualInputManager.Instance.dash = false;
            }
        }
    }
}


