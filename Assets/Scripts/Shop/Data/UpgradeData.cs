using UnityEngine;

namespace DSS.Shop
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "Shop/Upgrade")]
    public class UpgradeData : ScriptableObject
    {
        [field: Header("Enemy Specific Data")]
        
        [field: SerializeField] public string Name {get; private set;}
        [field: SerializeField] public string Description {get; private set;}
        [field: SerializeField] public int Cost {get; private set;}
        [field: SerializeField] public float Value {get; private set;}
        [field: SerializeField] public float CostMultiplier {get; private set;}
        [field: SerializeField] public float ValueMultiplier {get; private set;}
        [field: SerializeField] public UpgradeType upgradeType {get; private set;}
    }
}