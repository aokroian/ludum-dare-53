using System.Collections.Generic;
using UnityEngine;

namespace Actors.Upgrades
{
    public class DynamicActorStats : MonoBehaviour
    {
        [field: SerializeField] public ActorStatsSo ActorStatsSo { get; private set; }

        [field: Header("Actor Systems Refs")]
        [field: SerializeField] public ActorGunSystem ActorGunSystem { get; private set; }

        [field: SerializeField] public ActorHealth ActorHealthSystem { get; private set; }
        [field: SerializeField] public ActorMovement ActorMovementSystem { get; private set; }
        [field: SerializeField] public ActorScopeSystem ActorScopeSystem { get; private set; }

        private readonly List<IDynamicStatsReceiver> _dynamicStatsReceivers = new();

        private void Awake()
        {
            ActorStatsSo.OnValidateEvent += ApplyToAllReceivers;
        }

        private void ApplyToAllReceivers()
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