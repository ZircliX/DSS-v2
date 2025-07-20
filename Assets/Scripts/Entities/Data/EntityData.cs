using UnityEngine;

namespace DSS.Entities
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "DSS/Entities/EntityData", order = 1)]
    public class EntityData : ScriptableObject
    {
        [field: Header("Base Entity Data")]
        [field: SerializeField] public float Speed { get; private set; } = 1f;
        [field: SerializeField] public float Health { get; private set; } = 100f;
        
        [field: SerializeField] public float Damage { get; private set; } = 10f;
        [field: SerializeField] public float AttackRange { get; private set; } = 1f;
        [field: SerializeField] public float AttackCooldown { get; private set; } = 1f;
    }
}