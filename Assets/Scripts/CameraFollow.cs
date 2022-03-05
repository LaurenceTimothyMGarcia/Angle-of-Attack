using UnityEngine;

namespace CameraMovement {
    public class CameraFollow : MonoBehaviour
    {
        public static Transform ClientPlayer;
        public static bool followVelocity = false; // very buggy for now
        public float smoothSpeed = 0.125f;
        public Vector3 offset;

        private Vector3 pos = Vector3.zero;

        void AimCamera(Transform target = null) {
            if(target == null) {
                if(ClientPlayer == null) {
                    return;
                }
                target = ClientPlayer;
            }

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            transform.position = smoothedPosition;

            if(followVelocity) {
                transform.LookAt(target.position + (target.position - pos)/Time.deltaTime);
                pos = target.position;
            } else {
                transform.LookAt(target);
            }
        }

        // void LateUpdate() {
        //     AimCamera();
        // }

        void FixedUpdate() {
            AimCamera();
        }
    }
}
