using UnityEngine;

namespace Common.Player
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private Vector2 _movementDirection;
        private static readonly int AnimPlayerMove = Animator.StringToHash("AminPlayerMove");
        
        [SerializeField] private new Camera camera;
        [SerializeField] private float gunDistance = 0.6f;
        [SerializeField] private Transform gunTransform;
        [SerializeField] private float speedPlayerMovement = 5f;
        
        

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        
        public void Update()
        {
            ProcessInput();
            AnimatePLayer();
            RotateGunAroundPlayer();
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
       
       private void RotateGunAroundPlayer()
       {
           var position = transform.position;
           var mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
           var direction = (mousePosition - position).normalized;

           var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           gunTransform.rotation = Quaternion.Euler(0, 0, angle);
           
           var gunPosition = position + Quaternion.Euler(0.4f, 0, angle) * Vector3.right * gunDistance;
           gunPosition.z = -1;
           gunPosition.y -= 0.2f;
           gunTransform.position = gunPosition;
           FlipGun(angle);
       }
       
       private void FlipGun(float angle)
       {
           gunTransform.localScale = angle is > 90 or < -90 ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
       }
    }
}
