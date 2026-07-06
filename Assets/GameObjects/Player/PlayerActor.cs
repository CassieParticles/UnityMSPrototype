using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace GameObjects.Player
{
    [RequireComponent(typeof(Rigidbody2D),typeof(CapsuleCollider2D))]
    public class PlayerActor : MonoBehaviour
    {
        Rigidbody2D _rb;
        CapsuleCollider2D _capsuleCollider;

        [SerializeField] private float maxMoveSpeed;

        [SerializeField] private float jumpForce = 500;

        private Vector2 floorNormal;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
            _rb.freezeRotation = true;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            other.GetContacts(contacts);

            foreach (var contact in contacts)
            {
                floorNormal = (Vector2)transform.position - contact.point;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            floorNormal = Vector2.zero;
        }

        //-ve for left force
        public void ApplyMoveForce(float force)
        {
            //0 -> 1
            float maxSpeedScalar = Mathf.Abs(_rb.linearVelocityX) /  maxMoveSpeed;
            //1 -> 0
            maxSpeedScalar = 1 - maxSpeedScalar;

            if (Mathf.Sign(force) != Mathf.Sign(maxSpeedScalar) && maxSpeedScalar > 0.7)
            {
                //1 -> 2
                maxSpeedScalar = 2 - maxSpeedScalar;
            }
            
            Vector2 rightDirection = new Vector2(floorNormal.y, -floorNormal.x);
            
            _rb.AddForce(force * maxSpeedScalar * rightDirection );
        }

        public void ApplyJump()
        {
            _rb.AddForce(floorNormal * jumpForce);
        }
    }
}