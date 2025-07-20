using System;
using TMPro;
using UnityEngine;

namespace DSS.Shop
{
    public class ButtonUpgrade : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private UpgradeData Upgrade;
        
        [Header("Ui")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private TMP_Text valueText;

        public int Level;

        private void Awake()
        {
            UpdateUI();
        }

        public void TryToUpgrade()
        {
            int cost = Mathf.CeilToInt(Upgrade.Cost * Upgrade.CostMultiplier * Level);
            int value = Mathf.CeilToInt(Upgrade.Value * Upgrade.ValueMultiplier * Level);
            if (_shop.Upgrade(cost, Upgrade.upgradeType, value))
            {
                Level++;
                UpdateUI();
                Debug.Log(Level);
            }
        }

        public void UpdateUI()
        {
            titleText.text = Upgrade.Name;
            descriptionText.text = Upgrade.Description;
            levelText.text = "LvL:" + Level;
            costText.text = $"{Mathf.CeilToInt(Upgrade.Cost * Upgrade.CostMultiplier * Level).ToString()} $";
            valueText.text = $"+{Mathf.CeilToInt(Upgrade.Value * Upgrade.ValueMultiplier * Level).ToString()}";
        }
    }
}