using System;
using Sounds;
using UnityEngine;

namespace Actors
{
    public class ActorHealth : ActorSystem
    {
        public bool isDeathSound;
        public bool isDamageSound;
        public bool isHealSound;

        [field: SerializeField] public int MaxHealth { get; private set; } = 100;
        public int Health { get; private set; }
        public bool IsDead => Health <= 0;

        public event Action<ActorHealth> OnDeath;
        private event Action<ActorHealth> OnRevive;

        public event Action<float> OnHealthChanged;
        public event Action<float> OnHeal;
        public event Action<float> OnDamageTaken;

        protected override void Awake()
        {
            base.Awake();
            Health = MaxHealth;
        }

        public void Heal(int amount)
        {
            if (amount <= 0)
                return;
            var newHealth = Health + amount;

            if (IsDead && newHealth > 0)
                OnRevive?.Invoke(this);

            Health = newHealth;
            if (Health > MaxHealth)
                Health = MaxHealth;

            OnHealthChanged?.Invoke(Health);
            OnHeal?.Invoke(amount);
            if (isHealSound)
                SoundSystem.ActorHealSound(this);
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                return;

            if (IsDead)
            {
                if (isDeathSound)
                    SoundSystem.ActorDeathSound(this);
                OnDeath?.Invoke(this);
            }

            Health -= damage;
            if (Health < 0)
                Health = 0;

            OnHealthChanged?.Invoke(Health);
            OnDamageTaken?.Invoke(damage);
            if (isDamageSound)
                SoundSystem.ActorDamageSound(this);
        }
    }
}