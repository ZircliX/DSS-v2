using UnityEngine;

namespace DSS.Entities
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "DSS/Entities/EnemyData", order = 2)]
    public class EnemyData : EntityData
    {
        [field: Header("Enemy Specific Data")]
        [field: SerializeField] public float wanderRadius {get; private set;} = 10f;
        [field: SerializeField] public float wanderTime {get; private set;} = 3f;
        [field: SerializeField] public float detectionRadius {get; private set;} = 8f;
        [field: SerializeField] public float stoppingDistance {get; private set;} = 1.5f;
    }
}