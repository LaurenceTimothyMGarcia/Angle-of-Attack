using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace base_movement
{
    public class TestPlayer : MonoBehaviour
    {
        public float movementSpeed;

        void Update()
        {
            //if both left and right pressed at same time
            if (VirtualInputManager.Instance.moveRight && VirtualInputManager.Instance.moveLeft)
            {
                return;
            }

            if (VirtualInputManager.Instance.moveRight)
            {
                this.gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (VirtualInputManager.Instance.moveLeft)
            {
                this.gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }
}


