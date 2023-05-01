using System;
using Actors.Upgrades;
using Sounds;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actors
{
    public class ActorHealth : ActorSystem, IDynamicStatsReceiver
    {
        public float invincibilityTime;

        public bool isDeathSound;
        public bool isDamageSound;
        public bool isHealSound;
        [SerializeField] private int maxHealth;

        public bool IsDead => CurrentHealth <= 0;

        public event Action<ActorHealth> OnDeath;
        private event Action<ActorHealth> OnRevive;
        public event Action<int> OnHealthChanged;
        public event Action<int> OnHeal;
        public event Action<int> OnDamageTaken;
        public int CurrentMaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }

        private DynamicActorStats _dynamicActorStats;

        private float _invincibilityEndTime;

        protected override void Awake()
        {
            CurrentMaxHealth = maxHealth;
            CurrentHealth = CurrentMaxHealth; 

            base.Awake();

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
            CurrentMaxHealth = maxHealth + actorStatsSo.addedMaxHealth;
            OnHealthChanged?.Invoke(CurrentHealth);
        }

        public void Heal(int amount)
        {
            if (amount <= 0)
                return;
            var newHealth = CurrentHealth + amount;

            if (IsDead && newHealth > 0)
                OnRevive?.Invoke(this);

            CurrentHealth = newHealth;
            if (CurrentHealth > CurrentMaxHealth)
                CurrentHealth = CurrentMaxHealth;

            OnHealthChanged?.Invoke(CurrentHealth);
            OnHeal?.Invoke(amount);
            if (isHealSound)
                SoundSystem.ActorHealSound(this);
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0 || _invincibilityEndTime > Time.time)
                return;

            _invincibilityEndTime = Time.time + invincibilityTime;

            CurrentHealth -= damage;
            if (CurrentHealth < 0)
                CurrentHealth = 0;

            if (IsDead)
            {
                if (isDeathSound)
                    SoundSystem.ActorDeathSound(this);
                OnDeath?.Invoke(this);
            }

            OnHealthChanged?.Invoke(CurrentHealth);
            OnDamageTaken?.Invoke(damage);
            if (isDamageSound)
                SoundSystem.ActorDamageSound(this);
        }
    }
}