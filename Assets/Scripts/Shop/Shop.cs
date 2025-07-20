using DSS.Entities;
using UnityEngine;

namespace DSS.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private ShopUI shopUI; 
        public bool Upgrade(int Cost, UpgradeType type)
        {
            if (player == null) return false;

            if (player.Gold >= Cost)
            {
                // APPLY UPGRADE 
                player.Gold -= Cost;

                switch (type)
                {
                    case UpgradeType.PlayerSpeed:
                        Debug.Log("Player speed upgraded");
                        //DO PLAYER SPEED UPGRADE
                        break;
                    case UpgradeType.AttackSpeed:
                        Debug.Log("Player Attack speed upgraded");
                        //DO PLAYER ATTACK SPEED UPGRADE
                        break;
                    case UpgradeType.Damage:
                        Debug.Log("Player Damage upgraded");
                        //DO PLAYER DAMAGE UPGRADE
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