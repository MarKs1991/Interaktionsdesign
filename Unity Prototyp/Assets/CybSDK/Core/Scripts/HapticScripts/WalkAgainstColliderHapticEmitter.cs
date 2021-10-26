using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CybSDK
{
    /// <summary>
    /// Haptic is triggered when you walk against an obstacle. The haptic is activated when you walk against the obstacle for more than 0.5 seconds.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CVirtPlayerController))]
    public class WalkAgainstColliderHapticEmitter : CVirtHapticEmitter
    {
        private CharacterController characterController;
        private CVirtPlayerController playerController;

        // Velocity of character controller has to be different from Motion Vector of Virtualizer for at least this time
        private const float VerifyObstacleHitTime = 0.5f;
        private float verifyObstacleHitTimer;


        // Use this for initialization
        protected override void Start()
        {
            autoStart = false;
            base.Start();

            characterController = GetComponent<CharacterController>();
            playerController = GetComponent<CVirtPlayerController>();

            verifyObstacleHitTimer = VerifyObstacleHitTime;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 diffVector = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).normalized - new Vector3(playerController.MotionVector.x, 0.0f, playerController.MotionVector.z).normalized;

            if (diffVector.magnitude > 0.1f && playerController.MotionVector.magnitude > 0.1)
                verifyObstacleHitTimer -= Time.deltaTime;
            else
            {
                verifyObstacleHitTimer = VerifyObstacleHitTime;
                Stop();
            }

            if (verifyObstacleHitTimer < 0.0f)
                Play();
        }

        void OnDisable()
        {
            Stop();
        }
    }
}

