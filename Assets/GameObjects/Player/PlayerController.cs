using System;
using GameObjects.WeaponSystem.Bullet;
using GameObjects.WeaponSystem.WeaponState;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameObjects.Player
{
    [RequireComponent(typeof(PlayerActor),typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private PlayerActor _playerActor;

        [SerializeField] private Bullet bulletPrefab;
        
        private float _moveDirection = 0.0f;
        
        private Vector2 cursorPosition = Vector2.zero;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerActor = GetComponent<PlayerActor>();
        }

        private void OnEnable()
        {
            _playerInput.actions.FindAction("Move").performed += PlayerMove;
            _playerInput.actions.FindAction("Move").canceled += PlayerMove;
            
            _playerInput.actions.FindAction("Jump").performed += PlayerJump;
            
            _playerInput.actions.FindAction("Aim").performed += PlayerAim;
            
            _playerInput.actions.FindAction("Fire").performed += PlayerFire;
        }

        private void OnDisable()
        {
            _playerInput.actions.FindAction("Move").performed -= PlayerMove;
            _playerInput.actions.FindAction("Move").canceled -= PlayerMove;
            
            _playerInput.actions.FindAction("Jump").performed -= PlayerJump;
            
            _playerInput.actions.FindAction("Aim").performed -= PlayerAim;
            
            _playerInput.actions.FindAction("Fire").performed -= PlayerFire;
        }

        private void PlayerMove(InputAction.CallbackContext obj)
        {
            _moveDirection  = obj.action.ReadValue<float>();
        }

        private void PlayerJump(InputAction.CallbackContext obj)
        {
            _playerActor.ApplyJump();
        }

        private void PlayerAim(InputAction.CallbackContext obj)
        {
            cursorPosition = Camera.main.ScreenToWorldPoint(obj.ReadValue<Vector2>());
        }

        private void PlayerFire(InputAction.CallbackContext obj)
        {
            Vector2 direction = (cursorPosition - (Vector2)transform.position).normalized;
            WeaponState state = new WeaponState();
            
            state.bulletSize = 0.1f;
            state.bulletSpeed = 20;
            state.bulletDamage = 20;
            state.explosionRadius = 5;
            state.bounces = 2;
            state.gravityScale = 1;
            
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.ReadyBullet((Vector2)transform.position + direction, direction,state);
        }

        private void FixedUpdate()
        {
            _playerActor.ApplyMoveForce(_moveDirection * 15.0f);
        }
    }
}