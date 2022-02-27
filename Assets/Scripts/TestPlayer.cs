using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace base_movement
{
    public class TestPlayer : MonoBehaviour
    {
        public float movementSpeed;
        private float activeMovementSpeed;
        private float previousMovementSpeed;

        //Dash related
        public float dashSpeed;     //how fast is dash
        public float dashTime;      //how long dash last for
        public float dashCooldown;  //cooldown of dash
        public float dashLength;    //how long does dash travel

        private float dashTimeCounter;//dash time counter
        private float dashCoolCounter;//current cooldown of dash
        
        //Jump related
        public float jumpAmount;
        public int jumpCount;   //Extra Jumps
        public float fallingGravityScale;
        private int remainingJumps;
        private bool isGrounded;
        

        /*** Testing Gravity changes but might still use later
        public float gravityScale;
        //changes the scale so its less floaty
        public static float globalGravity = -9.81f;
        ***/

        public Rigidbody rb;

        //happens on start
        void OnEnable()
        {
            //rb.useGravity = false;  //removes current gravity system in unity to be replaced in fixedUpdate function
        }

        void Start()
        {
            activeMovementSpeed = movementSpeed;
            remainingJumps = jumpCount;
            dashTimeCounter = dashTime;
        }

        void Update()
        {
            //if both left and right pressed at same time
            if (VirtualInputManager.Instance.moveRight && VirtualInputManager.Instance.moveLeft)
            {
                return;
            }

            /***    Movement Horizontal    ***/
            if (VirtualInputManager.Instance.moveRight)
            {
                //this.gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

                //changed to force
                rb.AddForce(Vector3.forward * activeMovementSpeed);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (VirtualInputManager.Instance.moveLeft)
            {
                //this.gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

                //changed to force
                rb.AddForce(Vector3.back * activeMovementSpeed);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }


            /***    Dash    ***/
            /*if (VirtualInputManager.Instance.dash)
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMovementSpeed = dashSpeed;
                    dashCounter = dashLength;
                }

                
                if (rb.velocity.z > 0)
                {
                    rb.AddForce(Vector3.forward * dashSpeed, ForceMode.VelocityChange);
                }
                else if (rb.velocity.z < 0)
                {
                    rb.AddForce(Vector3.back * dashSpeed, ForceMode.VelocityChange);
                }
                
            }*/

            /*if (dashTimeCounter > 0)
            {
                dashTimeCounter -= Time.deltaTime;

                if (dashTimeCounter <= 0)
                {
                    activeMovementSpeed = movementSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }*/


            /***    Jumping     ***/
            if (VirtualInputManager.Instance.jump)
            {
                if (remainingJumps > 0)
                {
                    rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
                    remainingJumps--;
                    isGrounded = false;
                }

                Debug.Log(remainingJumps);
            }

            if (isGrounded)
            {
                remainingJumps = jumpCount;
            }

            //makes falling down faster than going up
            if (rb.velocity.y < 0)
            {
                rb.AddForce(Vector3.down * fallingGravityScale, ForceMode.Acceleration);
            }


            
            //Testing out gravity changes here but did not work as planned so scrapping for now
            //Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            //rb.AddForce(gravity, ForceMode.Acceleration);
            /*if (rb.velocity.y >= 0)
            {
                gravity = globalGravity * gravityScale * Vector3.up;
            }
            else if (rb.velocity.y < 0)
            {
                gravity = globalGravity * fallingGravityScale * Vector3.up;
            }*/
        }

        //Checks if player is on ground
        void OnCollisionStay()
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
    }
}


