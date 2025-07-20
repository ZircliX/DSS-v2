using DSS.Entities;
using UnityEngine;

namespace DSS
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private ParticleSystem hitEffect;
        
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        
        public void Initialize(EntityData entityData)
        {
            MaxHealth = Mathf.RoundToInt(entityData.Health);
            CurrentHealth = MaxHealth;
        }
        
        public void TakeDamage(int damage, Vector3 hitPoint)
        {
            CurrentHealth -= damage;
            
            PlayHitVFX(hitPoint);
            
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
        
        private void Die()
        {
            Destroy(gameObject);
        }
        
        public void Heal(int amount)
        {
            CurrentHealth += amount;
            CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
        }

        private void PlayHitVFX(Vector3 hitPoint)
        {
            ParticleSystem hitParticle = Instantiate(hitEffect, transform.position, Quaternion.identity);
            hitParticle.transform.parent = null;
            ParticleSystem.VelocityOverLifetimeModule velocity = hitParticle.velocityOverLifetime;

            Vector3 direction = (hitPoint - transform.position).normalized;
            
            velocity.enabled = true;
            velocity.space = ParticleSystemSimulationSpace.World;
            velocity.x = new ParticleSystem.MinMaxCurve(-direction.x * 5);
            velocity.y = new ParticleSystem.MinMaxCurve(-direction.y * 5);
            velocity.z = new ParticleSystem.MinMaxCurve(0);
            
            hitParticle.Play();
            Destroy(hitParticle.gameObject, hitParticle.main.duration);
        }
    }
}