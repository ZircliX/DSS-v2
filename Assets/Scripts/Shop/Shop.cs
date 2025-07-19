using DSS.Entities;
using UnityEngine;

namespace DSS.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private ShopUI shopUI; 
        public bool Upgrade(int Value)
        {
            if (player == null) return false;

            if (player.Gold >= Value)
            {
                //DEBITER LE JOUEUR / APPLY UPGRADE / UPDATE MULTIPLIER
                player.Gold -= Value;
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