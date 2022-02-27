using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace base_movement
{
    public class TestPlayer : MonoBehaviour
    {
        public float movementSpeed;
        public float dashSpeed;
        public float dashTime;
        
        //jump related
        public float jumpAmount;
        public float gravityScale;
        public float fallingGravityScale;   //changes the scale so its less floaty
        public static float globalGravity = -9.81f;

        public Rigidbody rb;

        //happens on start
        void OnEnable()
        {
            //rb.useGravity = false;  //removes current gravity system in unity to be replaced in fixedUpdate function
        }

        void Update()
        {
            //if both left and right pressed at same time
            if (VirtualInputManager.Instance.moveRight && VirtualInputManager.Instance.moveLeft)
            {
                return;
            }

            if (VirtualInputManager.Instance.moveRight)
            {
                //this.gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

                //changed to force
                rb.AddForce(Vector3.forward * movementSpeed);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (VirtualInputManager.Instance.moveLeft)
            {
                //this.gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

                //changed to force
                rb.AddForce(Vector3.back * movementSpeed);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }

            //Dash
            if (VirtualInputManager.Instance.dash)
            {
                if (rb.velocity.z > 0)
                {
                    rb.AddForce(Vector3.forward * dashSpeed, ForceMode.VelocityChange);
                }
                else if (rb.velocity.z < 0)
                {
                    rb.AddForce(Vector3.back * dashSpeed, ForceMode.VelocityChange);
                }
            }

            //Jump button
            //Vector3 gravity = globalGravity * gravityScale * Vector3.up;

            //rb.AddForce(gravity, ForceMode.Acceleration);
            if (VirtualInputManager.Instance.jump)
            {
                rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            }
            
            /*if (rb.velocity.y >= 0)
            {
                gravity = globalGravity * gravityScale * Vector3.up;
            }
            else if (rb.velocity.y < 0)
            {
                gravity = globalGravity * fallingGravityScale * Vector3.up;
            }*/
        }
    }
}


