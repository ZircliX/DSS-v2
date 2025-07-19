using System.Collections.Generic;
using DSS.Entities;
using UnityEngine;

namespace DSS
{
    public class Attack : MonoBehaviour
    {
        public EntityData EntityData { get; private set; }

        public float CooldownTimer { get; private set; }

        public void Initialize(EntityData data)
        {
            EntityData = data;
            CooldownTimer = 0;
        }

        public void PerformAttack(params Health[] targets)
        {
            if (CooldownTimer > 0)
                return;

            for (int i = 0; i < targets.Length; i++)
            {
                Health target = targets[i];
                if (target != null && Vector3.Distance(transform.position, target.transform.position) <= EntityData.AttackRange)
                {
                    //Debug.Log($"Attacking {target.gameObject.name} for {EntityData.Damage} damage.");
                    target.TakeDamage(Mathf.RoundToInt(EntityData.Damage));

                    // Optional knockback:
                    // Vector3 knockbackDir = (target.transform.position - transform.position).normalized;
                    // target.GetComponent<Rigidbody2D>()?.AddForce(knockbackDir * EntityData.KnockbackForce, ForceMode2D.Impulse);
                }
            }

            CooldownTimer = EntityData.AttackCooldown;
        }

        public void PerformAttack(List<Health> targets)
        {
            PerformAttack(targets.ToArray());
        }
        
        private void Update()
        {
            if (CooldownTimer > 0f)
                CooldownTimer -= Time.deltaTime;
        }
    }
}