using System;
using DSS.Entities;
using UnityEngine;

namespace DSS
{
    public class Health : MonoBehaviour
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        
        public void Initialize(EntityData entityData)
        {
            MaxHealth = Mathf.RoundToInt(entityData.Health);
            CurrentHealth = MaxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
        
        private void Die()
        {
            Debug.Log($"{gameObject.name} has died.");
            GameManager.Instance.EndGame();
        }
        
        public void Heal(int amount)
        {
            CurrentHealth += amount;
            CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
        }

        public void ResurrectPlayer()
        {
            Heal(MaxHealth);
        }
    }
}