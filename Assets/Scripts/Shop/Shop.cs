using DSS.Entities;
using UnityEngine;

namespace DSS.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopUI shopUI; 
        public bool Upgrade(int Cost, UpgradeType type, float Value)
        {
            if (Player.Instance.Gold>= Cost)
            {
                // APPLY UPGRADE 
                Player.Instance.Gold-= Cost;

                switch (type)
                {
                    case UpgradeType.PlayerSpeed:
                        Debug.Log("Player speed upgraded");
                        Player.Instance.UpdateMovementSpeed(Value);
                        break;
                    case UpgradeType.AttackSpeed:
                        Debug.Log("Player Attack speed upgraded");
                        Debug.LogError(Value);
                        Player.Instance.Attack.UpdatePlayerCooldownTimer(Value);
                        break;
                    case UpgradeType.Damage:
                        Debug.Log("Player Damage upgraded");
                        Player.Instance.Attack.UpdateBonusDamage(Value);
                        break;
                }
                return true;
            }
            else
            {
                shopUI.DoErrorToUpgrade();
                return false;
            }
        }
    }
}