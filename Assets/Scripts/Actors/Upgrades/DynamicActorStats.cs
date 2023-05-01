using System.Collections.Generic;
using Actors.ActorSystems;
using UnityEngine;

namespace Actors.Upgrades
{
    public class DynamicActorStats : MonoBehaviour
    {
        [field: SerializeField] public ActorStatsSo ActorStatsSo { get; private set; }

        [field: Header("Actor Systems Refs")]
        [field: SerializeField] public ActorGunSystem ActorGunSystem { get; private set; }

        [field: SerializeField] public ActorHealth ActorHealthSystem { get; private set; }
        [field: SerializeField] public ActorPhysics ActorPhysicsSystem { get; private set; }
        [field: SerializeField] public ActorScopeSystem ActorScopeSystem { get; private set; }

        private readonly List<IDynamicStatsReceiver> _dynamicStatsReceivers = new();

        private void Awake()
        {
            var copy = ActorStatsSo.GetCopy();
            ActorStatsSo = copy;
            ActorStatsSo.OnValidateEvent += ApplyStatsToAllReceivers;
        }

        public void ModifyCurrentStatsSo(ActorStatsSo actorStatsToAdd)
        {
            ActorStatsSo.addedMovementSpeed += actorStatsToAdd.addedMovementSpeed;
            ActorStatsSo.addedScaleModifier += actorStatsToAdd.addedScaleModifier; 
            ActorStatsSo.addedMaxHealth += actorStatsToAdd.addedMaxHealth;
            ActorStatsSo.addedShootRate += actorStatsToAdd.addedShootRate;
            ActorStatsSo.addedBulletsSpeed += actorStatsToAdd.addedBulletsSpeed;
            ActorStatsSo.addedBulletsDamage += actorStatsToAdd.addedBulletsDamage;
            ActorStatsSo.addedBulletsScale += actorStatsToAdd.addedBulletsScale;
            ActorStatsSo.addedBulletsPerShotCount += actorStatsToAdd.addedBulletsPerShotCount;
            ActorStatsSo.addedBulletsPiercingCount += actorStatsToAdd.addedBulletsPiercingCount;
            ApplyStatsToAllReceivers();
        }

        private void ApplyStatsToAllReceivers()
        {
            foreach (var receiver in _dynamicStatsReceivers)
                receiver.ApplyDynamicStats(ActorStatsSo);
        }

        public void AddReceiver(IDynamicStatsReceiver dynamicStatsReceiver)
        {
            if (_dynamicStatsReceivers.Contains(dynamicStatsReceiver))
                return;
            _dynamicStatsReceivers.Add(dynamicStatsReceiver);
            dynamicStatsReceiver.ApplyDynamicStats(ActorStatsSo);
        }

        public void RemoveReceiver(IDynamicStatsReceiver dynamicStatsReceiver)
        {
            if (_dynamicStatsReceivers.Contains(dynamicStatsReceiver))
                _dynamicStatsReceivers.Remove(dynamicStatsReceiver);
        }
    }
}