using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CybSDK
{
    /// <summary>
    /// Haptic is triggered when the player lands on the floor after falling/jumping from higher ground. The player has to fall for more than 0.1 seconds to activate the haptic
    /// feedback once reaching the ground. 
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CVirtPlayerController))]
    public class FallImpactHapticEmitter : CVirtHapticEmitter
    {

        private CharacterController characterController;
        private CVirtPlayerController playerController;

        private bool isFalling = false;

        //GameObject has to fall for at least this time before haptic is activated
        private const float VerifyFallTime = 0.1f;
        private float verifyFallTimer;

        // Use this for initialization
        protected override void Start()
        {
            autoStart = false;
            base.Start();

            characterController = GetComponent<CharacterController>();
            playerController = GetComponent<CVirtPlayerController>();

            verifyFallTimer = VerifyFallTime;
        }

        // Update is called once per frame
        void Update()
        {

            if (characterController.velocity.y < -3.0f)
            {
                verifyFallTimer -= Time.deltaTime;
            }
            else
            {
                verifyFallTimer = VerifyFallTime;

                if (isFalling)
                {
                    StartCoroutine("PlayHaptic");
                    isFalling = false;
                }
            }

            if (verifyFallTimer < 0.0f)
            {
                isFalling = true;
            }


        }

        void OnDisable()
        {
            keepActive = false;
            Stop();
        }

        private IEnumerator PlayHaptic()
        {
            keepActive = true;
            Play();
            yield return new WaitForSeconds(Duration);
            keepActive = false;
            Stop();
        }
    }
}
