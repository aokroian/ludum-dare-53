using System;
using Actors.Upgrades;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actors
{
    public class ActorPhysics : ActorSystem, IDynamicStatsReceiver
    {
        [SerializeField] private float speed = 5f;
        private Rigidbody2D _rigidbody2D;

        private float _currentSpeed;
        public float DefaultScale { get; private set; }
        private float _currentScale;
        private DynamicActorStats _dynamicActorStats;


        protected override void Awake()
        {
            _currentSpeed = speed;
            DefaultScale = transform.localScale.x;
            _currentScale = DefaultScale;

            base.Awake();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _dynamicActorStats = GetComponent<DynamicActorStats>();
            if (_dynamicActorStats != null)
                _dynamicActorStats.AddReceiver(this);
        }

        private void OnDestroy()
        {
            if (_dynamicActorStats != null)
                _dynamicActorStats.RemoveReceiver(this);
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