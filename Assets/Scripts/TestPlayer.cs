using CameraMovement;
using Unity.Netcode;
using UnityEngine;

namespace base_movement
{
    public class TestPlayer : NetworkBehaviour
    {
        public float maxMovementSpeed;
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
        private Vector3 tempVelocity; //speed Before the dash
        
        //Jump related
        public float jumpAmount; // jump velocity
        public int jumpCount;   //Extra Jumps
        public float fallingGravityScale;
        private int remainingJumps;
        private bool isGrounded;

        // private const bool isServer = singleton.isServer;
        

        /*** Testing Gravity changes but might still use later
        public float gravityScale;
        //changes the scale so its less floaty
        public static float globalGravity = -9.81f;
        ***/

        private Rigidbody rb;

        //happens on start
        void OnEnable()
        {
            //rb.useGravity = false;  //removes current gravity system in unity to be replaced in fixedUpdate function
        }

        void Start()
        {
            rb = this.gameObject.GetComponent<Rigidbody>();
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

            //Cap velocity
            if (rb.velocity.magnitude >  maxMovementSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMovementSpeed);
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
            // TODO-RAY Will need to make networked method for assigning rigidbody velocity
            if (VirtualInputManager.Instance.dash && dashCoolCounter <= 0)
            {
                tempVelocity = rb.velocity;
                
                if (VirtualInputManager.Instance.moveRight)
                {
                    rb.velocity = Vector3.forward * dashSpeed;
                }
                else if (VirtualInputManager.Instance.moveLeft)
                {
                    rb.velocity = Vector3.back * dashSpeed;
                }

                dashCoolCounter = dashCooldown;
            }

            if (dashTimeCounter > 0)
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
            }


            /***    Jumping     ***/
            if (VirtualInputManager.Instance.jump)
            {
                if (remainingJumps > 0)
                {
                    rb.velocity = Vector3.up * jumpAmount;
                    remainingJumps--;
                    isGrounded = false;
                }
            }

            if (isGrounded)
            {
                remainingJumps = jumpCount;
            }

            //makes falling down faster than going up
            if (rb.velocity.y < 0)
            {
                rb.AddForce(Vector3.down * fallingGravityScale, ForceMode.Acceleration);
                // if (rb.velocity.magnitude >  maxMovementSpeed)
                // {
                //     rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMovementSpeed);
                // }
                
            }

            /***    Camera Movement    ***/
            if (IsOwner) {
                CameraFollow.ClientPlayer = this.transform;
            }

            //Testing out gravity changes here but did not work as planned so scrapping for now
            //Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            //PlayerForce(gravity, ForceMode.Acceleration);
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
        }

        public void PlayerForce(Vector3 direction, ForceMode mode = ForceMode.Force) {
            if(IsOwner) {
                if(NetworkManager.Singleton.IsServer) {
                    rb.AddForce(direction, mode);
                } else {
                    RequestPlayerForceServerRpc(direction, mode);
                }
            }
        }

        [ServerRpc]
        void RequestPlayerForceServerRpc(Vector3 direction, ForceMode mode) {
            rb.AddForce(direction, mode);
        }
    }
}


