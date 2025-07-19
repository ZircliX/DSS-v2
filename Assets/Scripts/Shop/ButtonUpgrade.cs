using UnityEngine;

namespace DSS.Shop
{
    public class ButtonUpgrade
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private UpgradeData Upgrade;

        public int Level;

        public void TryToUpgrade()
        {
            if (_shop.Upgrade(Mathf.CeilToInt(Upgrade.Value * Upgrade.Multiplier * Level)))
            {
                Level++;
            }
        }
    }
}