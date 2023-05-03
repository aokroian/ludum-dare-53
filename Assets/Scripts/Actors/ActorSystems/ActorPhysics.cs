using Actors.Upgrades;
using UnityEngine;

namespace Actors.ActorSystems
{
    public class ActorPhysics : ActorSystem, IActorStatsReceiver
    {
        [SerializeField] private float speed = 5f;
        private Rigidbody2D _rigidbody2D;

        private float _currentSpeed;
        public float DefaultScale { get; private set; }
        private float _currentScale;
        private ActorStatsController _actorStatsController;


        protected override void Awake()
        {
            _currentSpeed = speed;
            DefaultScale = transform.localScale.x;
            _currentScale = DefaultScale;

            base.Awake();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _actorStatsController = GetComponent<ActorStatsController>();
            if (_actorStatsController != null)
                _actorStatsController.AddReceiver(this);
        }

        private void OnDestroy()
        {
            if (_actorStatsController != null)
                _actorStatsController.RemoveReceiver(this);
        }

        public void ApplyDynamicStats(ActorStatsSo actorStatsSo)
        {
            _currentSpeed = speed + actorStatsSo.addedMovementSpeed;
            _currentScale = DefaultScale + actorStatsSo.addedScaleModifier;

            transform.localScale = new Vector3(_currentScale, _currentScale, 1);
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            _rigidbody2D.velocity = new Vector2(
                ActorInput.Movement.x * _currentSpeed,
                ActorInput.Movement.y * _currentSpeed);
        }
    }
}