using DSS.Entities;
using DSS.Shop;
using LTX.Singletons;
using UnityEngine;

namespace DSS
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private ShopUI shopUI;

        protected override void Awake()
        {
            base.Awake();
            
        }

        public void StartGame()
        {
            //LANCER LA GAME AVEC LES STATS DE BASE DU JOUEUR (game start from MainMenu)
            //CACHER LE MAIN MENU !
        }

        public void EndGame()
        {
            // LE PLAYER EST MORT FAIRE SPAWN LE SHOP !
            // FREEZE LA GAME / LE TIMER
        }

        public void RestartGame()
        {
            // REFAIRE SPAWN LE JOUEUR
            // DELETE MOBS AND REPAWN THEM
            // RESET TIMER !
        }
        
        #region SHOP
        
        private void OpenShop()
        {
            if (shopUI != null)
                shopUI.OpenShop();
        }
        
        private void CloseShop()
        {
            if (shopUI != null)
                shopUI.CloseShop();
        }
        
        #endregion
    }
}