using System;
using System.Collections;
using System.Collections.Generic;
using CybSDK;
using UnityEngine;

namespace CybSDK
{
    /// <summary>
    /// Haptic is triggered when the player is hit by an object. The object hitting the player has to call the HitImpact method of this script.
    /// </summary>
    public class PlayerHitHapticEmitter : CVirtHapticEmitter
    {

        // Use this for initialization
        protected override void Start()
        {
            autoStart = false;
            base.Start();
        }

        public void HitImpact()
        {
            StartCoroutine("Impact");
        }

        private IEnumerator Impact()
        {
            keepActive = true;
            Play();
            yield return new WaitForSeconds(Duration);
            keepActive = false;
            Stop();
        }

        void OnDisable()
        {
            keepActive = false;
            Stop();
        }
    }
}
