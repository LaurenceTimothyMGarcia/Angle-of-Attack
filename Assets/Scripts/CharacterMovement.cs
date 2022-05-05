using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraMovement;

namespace base_movement
{
    public class CharacterMovement : MonoBehaviour
    {
        public float movementSpeed = 2.5f;
        public float maxMovementSpeed = 50f;
        public float jumpHeight = 5f;
        private float activeMovementSpeed = 50f;
        private float previousMovementSpeed;

        public Transform groundCheckTransform;
        public float groundCheckRadius = 0.2f;

        public Transform targetTransform;

        public GameObject bulletPrefab;
        public Transform shootPoint;

        private float inputMovement;
        private Animator animator;
        private Rigidbody rb;
        private bool isGrounded;
        public LayerMask groundMask;

        //Check if looking left or right
        private int facingSign
        {
            get
            {
                Vector3 perp = Vector3.Cross(transform.forward, Vector3.forward);
                float dir = Vector3.Dot(perp, transform.up);
                return dir > 0f ? -1 : dir < 0f ? 1 : 0;
            }
        }

        private Camera mainCamera;
        public LayerMask mouseAimMask;

        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            mainCamera = Camera.main;

            activeMovementSpeed = movementSpeed;
        }

        void Update()
        {
            /*** RAYCASTING FOR MOUSE DIRECTION ***/
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
            {
                targetTransform.position = hit.point;
            }

            /*** Horizontal Movement ***/
            if (VirtualInputManager.Instance.moveRight && VirtualInputManager.Instance.moveLeft)
            {
                return;
            }

            if (rb.velocity.magnitude > maxMovementSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMovementSpeed);
            }

            if (VirtualInputManager.Instance.moveRight)
            {
                rb.AddForce(Vector3.forward * activeMovementSpeed);
            }

            if (VirtualInputManager.Instance.moveLeft)
            {
                rb.AddForce(Vector3.back * activeMovementSpeed);
            }

            animator.SetFloat("Speed", rb.velocity.magnitude);


            /*** Direction of character ***/
            //Character faces in direction of mouse
            rb.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(targetTransform.position.z - transform.position.z), 0)));

            /*** JUMPING ***/
            if (VirtualInputManager.Instance.jump && isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
                rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -1 * Physics.gravity.y), ForceMode.VelocityChange);
            }

            //Ground Check
            isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);
            animator.SetBool("isGrounded", isGrounded);

            /*** SHOOTING ***/
            if (VirtualInputManager.Instance.shoot)
            {
                Fire();
            }
        }

        void Fire()
        {
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }

        private void OnAnimatorIK()
        {
            //AIM AT TARGET IK
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);

            // Look at target IK
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(targetTransform.position);
        }
    }
}
    
