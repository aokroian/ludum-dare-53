using System.Collections.Generic;
using Actors;
using UnityEngine;

namespace UI.Hud
{
    public class PlayerHealthHud : MonoBehaviour
    {
        [SerializeField] private int healthInHeart = 2;
        [SerializeField] private GameObject heartPrefab;

        private int curMaxHealth;
        private List<PlayerHeart> _hearts = new();

        private void Awake()
        {
            var playerHealth = GetComponentInParent<ActorHealth>();
            SetHealth(playerHealth.MaxHealth, playerHealth.Health);
            playerHealth.OnHealthChanged +=
                curHealth => SetHealth(playerHealth.MaxHealth, curHealth);
        }

        private void SetHealth(int maxHealth, int health)
        {
            if (curMaxHealth != maxHealth)
            {
                RecreateHearts(health / healthInHeart);
            }
            
            FillHearts(health);
        }

        private void RecreateHearts(int hearts)
        {
            _hearts.Clear();
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (var i = 0; i < hearts; i++)
            {
                var obj = Instantiate(heartPrefab, transform);
                _hearts.Add(obj.GetComponent<PlayerHeart>());
            }
        }

        private void FillHearts(int health)
        {
            // TODO: rewrite with one cycle
            
            var fullHearts = Mathf.FloorToInt(health / healthInHeart);
            for (int i = 0; i < fullHearts; i++)
            {
                _hearts[i].SetFill(1);
            }

            var healthLeft = health % healthInHeart;
            if (healthLeft > 0)
            {
                _hearts[fullHearts].SetFill(healthLeft / (float) healthInHeart);
            }

            for (int i = fullHearts + 1; i < _hearts.Count; i++)
            {
                _hearts[i].SetFill(0);
            }
        }
    }
}