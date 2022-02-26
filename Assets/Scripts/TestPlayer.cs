using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace base_movement
{
    public class TestPlayer : MonoBehaviour
    {
        public float movementSpeed;
        
        //jump related
        public float jumpAmount;
        public float gravityScale;
        public float fallingGravityScale;   //changes the scale so its less floaty
        public static float globalGravity = -9.81f;

        public Rigidbody rb;

        //happens on start
        void OnEnable()
        {
            rb.useGravity = false;  //removes current gravity system in unity to be replaced in fixedUpdate function
        }

        void FixedUpdate()
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

            //Jump button
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;

            rb.AddForce(gravity, ForceMode.Acceleration);
            if (VirtualInputManager.Instance.jump)
            {
                rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            }
            if (rb.velocity.y >= 0)
            {
                gravity = globalGravity * gravityScale * Vector3.up;
            }
            else if (rb.velocity.y < 0)
            {
                gravity = globalGravity * fallingGravityScale * Vector3.up;
            }
        }
    }
}


