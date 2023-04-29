using System;
using TMPro;
using UnityEngine;

namespace Common.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private Vector2 _movementDirection;
        [SerializeField] private float speedPlayerMovement = 5f;
        private static readonly int AnimPlayerMove = Animator.StringToHash("AminPlayerMove");

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        
        public void Update()
        {
            ProcessInput();
            AnimatePLayer();
        }

        private void FixedUpdate()
        {
            MovementPlayer();
            
        }

       private void ProcessInput()
        {
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveY = Input.GetAxisRaw("Vertical");
            
            _movementDirection = new Vector2(moveX, moveY).normalized;
        }

       private void MovementPlayer()
       {
           _rigidbody2D.velocity = new Vector2(_movementDirection.x * speedPlayerMovement,
               _movementDirection.y * speedPlayerMovement);
       }

       private void AnimatePLayer()
       {
           var direction = 0;

           if (_movementDirection.x > 0)
           {
               direction = 4;
           }
           else if (_movementDirection.x < 0)
           {
               direction = 3; 
           }
           else if (_movementDirection.y > 0)
           {
               direction = 2; 
           }
           else if (_movementDirection.y < 0)
           {
               direction = 1; 
           }

           _animator.SetInteger(AnimPlayerMove, direction);
       }
        
    }
}
