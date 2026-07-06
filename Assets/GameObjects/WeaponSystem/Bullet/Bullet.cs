using System;
using UnityEngine;
using GameObjects.WeaponSystem.WeaponState;

namespace GameObjects.WeaponSystem.Bullet
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        public float moveSpeed;
        public Vector2 direction;
        
        public void ReadyBullet(Vector2 origin, Vector2 direction, WeaponState.WeaponState weaponState)
        {
            gameObject.SetActive(true);
            
            transform.position = origin;
            this.direction = direction.normalized;
            
            moveSpeed = weaponState.bulletSpeed;

            GetComponent<CircleCollider2D>().radius = weaponState.bulletSize;
            GetComponent<Rigidbody2D>().linearVelocity = direction * moveSpeed;
            
            GetComponentInChildren<AExplosion>().ReadyExplosion(weaponState.explosionRadius / weaponState.bulletSize);
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Uh oh, I hit someone");
            
            GetComponentInChildren<AExplosion>().Explode();
            Destroy(gameObject);
        }
    }
}