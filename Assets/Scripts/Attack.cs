using System.Collections.Generic;
using DSS.Entities;
using DSS.Sounds;
using UnityEngine;

namespace DSS
{
    public class Attack : MonoBehaviour
    {
        public EntityData EntityData { get; private set; }

        public float CooldownTimer { get; private set; }

        private float _bonusDamage;
        private float _cooldownReduction;

        public void Initialize(EntityData data)
        {
            EntityData = data;
            CooldownTimer = 0;
        }

        public void UpdateBonusDamage(float bonusDamage)
        {
            Debug.Log($"_bonusDamage before: {_bonusDamage}");
            _bonusDamage += bonusDamage;
            Debug.Log($"_bonusDamage after: {_bonusDamage}");
        }

        public void UpdatePlayerCooldownTimer(float cooldownReduction)
        {
            Debug.Log($"Cooldown before: {CooldownTimer}");
            _cooldownReduction += cooldownReduction;
            Debug.Log($"Cooldown after: {CooldownTimer}");
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
                    target.TakeDamage(Mathf.RoundToInt(EntityData.Damage), transform.position);

                    // Optional knockback:
                    // Vector3 knockbackDir = (target.transform.position - transform.position).normalized;
                    // target.GetComponent<Rigidbody2D>()?.AddForce(knockbackDir * EntityData.KnockbackForce, ForceMode2D.Impulse);
                }
            }

            CooldownTimer = EntityData.AttackCooldown - _cooldownReduction;
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