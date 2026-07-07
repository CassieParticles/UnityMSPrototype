using System;
using UnityEngine;
using GameObjects.WeaponSystem.WeaponState;

namespace GameObjects.WeaponSystem.Bullet
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        Rigidbody2D rigidBody;
        CircleCollider2D circleCollider;
        
        public float moveSpeed;
        public Vector2 direction;

        public int bounces;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            circleCollider = GetComponent<CircleCollider2D>();
        }

        public void ReadyBullet(Vector2 origin, Vector2 direction, WeaponState.WeaponState weaponState)
        {
            gameObject.SetActive(true);
            
            transform.position = origin;
            this.direction = direction.normalized;
            
            moveSpeed = weaponState.bulletSpeed;

            circleCollider.radius = weaponState.bulletSize;
            rigidBody.linearVelocity = direction * moveSpeed;
            rigidBody.gravityScale = weaponState.gravityScale;

            bounces = weaponState.bounces;
            
            GetComponentInChildren<AExplosion>().ReadyExplosion(weaponState.explosionRadius / weaponState.bulletSize);
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //If it should bounce, reflect based on normal
            bool bounced = bounces > 0;
            if (bounced)
            {
                rigidBody.AddForce(other.GetContact(0).normalImpulse * other.GetContact(0).normal,ForceMode2D.Impulse);

                bounces--;
            }
            
            GetComponentInChildren<AExplosion>().Explode();

            if (!bounced)
            {
                Destroy(gameObject);
            }
        }
    }
}