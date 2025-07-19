using DSS.Entities;
using UnityEngine;

namespace DSS
{
    public class Attack : MonoBehaviour
    {
        private EntityData entityData;

        private float lastAttackTime;

        public void Initialize(EntityData data)
        {
            entityData = data;
            lastAttackTime = -entityData.AttackCooldown; // Allow immediate attack
        }

        public void PerformAttack(Health target)
        {
            if (target == null || target.CurrentHealth <= 0)
                return;
            
            if (Time.time - lastAttackTime < entityData.AttackCooldown)
                return;
            
            Debug.Log( $"Attacking {target.gameObject.name} for {entityData.Damage} damage.");

            Transform targetTransform = target.transform;
            float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
            if (distanceToTarget <= entityData.AttackRange)
            {
                target.TakeDamage(Mathf.RoundToInt(entityData.Damage));
                //Vector3 knockbackDirection = (targetTransform.position - transform.position).normalized;
                //target.GetComponent<Rigidbody>().AddForce(knockbackDirection * entityData.KnockbackForce, ForceMode.Impulse);
            }

            lastAttackTime = Time.time;
        }
    }
}