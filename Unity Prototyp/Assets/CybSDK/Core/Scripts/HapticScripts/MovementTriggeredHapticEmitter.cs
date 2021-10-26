using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CybSDK
{
    /// <summary>
    /// Haptic is triggered when the object this script is attached to moves.
    /// </summary>
    public class MovementTriggeredHapticEmitter : CVirtHapticEmitter
    {
        private Vector3 oldPosition;

        // Use this for initialization
        protected override void Start()
        {
            autoStart = false;
            base.Start();

            oldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (oldPosition != transform.position)
            {
                Play();
            }
            else
            {
                Stop();
            }

            oldPosition = transform.position;
        }

        void OnDisable()
        {
            Stop();
        }
    }
}


