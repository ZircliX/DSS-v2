using UnityEngine;
using UnityEngine.UI;

namespace DSS.Entities
{
    public class EntityUI : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Attack attack;
        
        [SerializeField] private Image healthBar;
        [SerializeField] private Image attackCooldownBar;

        private void Update()
        {
            healthBar.fillAmount = (float)health.CurrentHealth / (float)health.MaxHealth;
            attackCooldownBar.fillAmount = attack.CooldownTimer / attack.EntityData.AttackCooldown;
        }
    }
}