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
        }
    }
}


