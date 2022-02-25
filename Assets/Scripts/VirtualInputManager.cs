using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace base_movement
{
    public class VirtualInputManager : MonoBehaviour
    {
        public static VirtualInputManager Instance = null;

        private void Awake()
        {
            //following elif statement makes only 1 input manager at a time
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        public bool moveRight;
        public bool moveLeft;
    }
}

