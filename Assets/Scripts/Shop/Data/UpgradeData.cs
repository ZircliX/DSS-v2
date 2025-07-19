using UnityEngine;

namespace DSS.Shop
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "Shop/Upgrade")]
    public class UpgradeData : ScriptableObject
    {
        [field: Header("Enemy Specific Data")]
        
        [field: SerializeField] public string Name {get; private set;}
        [field: SerializeField] public string Description {get; private set;}
        [field: SerializeField] public string Cost {get; private set;}
        [field: SerializeField] public int Value {get; private set;}
        [field: SerializeField] public float Multiplier {get; private set;}
    }
}