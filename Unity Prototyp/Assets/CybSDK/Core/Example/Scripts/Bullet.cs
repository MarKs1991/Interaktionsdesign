using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CybSDK
{
    public class Bullet : MonoBehaviour
    {
        [HideInInspector]
        public PlayerHitHapticEmitter hapticPlayerHitScript;

        private Collider playerHitCollider;

        void OnTriggerEnter(Collider collider)
        {
            playerHitCollider = hapticPlayerHitScript.gameObject.GetComponent<Collider>();

            if (playerHitCollider == collider)
                hapticPlayerHitScript.HitImpact();

            Destroy(gameObject);

        }
    }
}

