using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CybSDK
{
    public class BulletSpawn : MonoBehaviour
    {

        public GameObject BulletPrefab;
        public float BulletSpeed = 100.0f;

        public PlayerHitHapticEmitter HapticPlayerHitScript;

        private const float BulletShootTime = 5.0f;
        private float bulletShootTimer;


        // Update is called once per frame
        void Update()
        {
            bulletShootTimer -= Time.deltaTime;

            if (bulletShootTimer < 0.0f)
            {
                ShootBullet();
                bulletShootTimer = BulletShootTime;
            }
        }

        void ShootBullet()
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.up * BulletSpeed);
            bullet.GetComponent<Bullet>().hapticPlayerHitScript = HapticPlayerHitScript;
        }
    }
}


