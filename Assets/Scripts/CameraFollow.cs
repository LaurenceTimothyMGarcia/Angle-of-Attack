using UnityEngine;

namespace CameraMovement {
    public class CameraFollow : MonoBehaviour
    {
        public static Transform ClientPlayer;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;

        private Vector3 pos = Vector3.zero;
        private static float standardFov = 60f;
        private static float velocityCamOffsetSensetivity = 10f;
        private static float velocityFovSensetivity = 2f;

        void AimCamera(Transform target = null) {
            if(target == null) {
                if(ClientPlayer == null) {
                    return;
                }
                target = ClientPlayer;
            }

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            Vector3 velocity = target.GetComponent<Rigidbody>().velocity;
            
            transform.position = smoothedPosition;
            transform.LookAt(target.transform.position + velocity / velocityCamOffsetSensetivity);

            // adjusts camera FOV
            Camera.main.fieldOfView = standardFov + Mathf.Clamp(velocity.magnitude / velocityFovSensetivity, 0, standardFov/4f);
            Debug.Log(Camera.main.fieldOfView);
        }

        // void LateUpdate() {
        //     AimCamera();
        // }

        void FixedUpdate() {
            AimCamera();
        }
    }
}
