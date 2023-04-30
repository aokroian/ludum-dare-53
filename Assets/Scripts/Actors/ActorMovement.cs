using Actors.InputThings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actors
{
    public class ActorMovement : ActorSystem
    {
        [SerializeField] private float speed = 5f;
        private Rigidbody2D _rigidbody2D;

        protected override void Awake()
        {
            base.Awake();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            _rigidbody2D.velocity = new Vector2(
                ActorInput.Movement.x * speed,
                ActorInput.Movement.y * speed);
        }
    }
}