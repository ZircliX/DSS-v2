using System;
using DSS.Shop;
using DSS.Timer;
using LTX.Singletons;
using UnityEngine;
using UnityEngine.Video;

namespace DSS
{
    public class GameManager : SceneSingleton<GameManager>
    {
        [SerializeField] private ShopUI shopUI;
        [SerializeField] private Health health;

        [SerializeField] private TimerManager timer;
        
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform spawnPoint;

        private string _currentTimer;

        private void Start()
        {
            Time.timeScale = 0f;
        }

        public void StartGame()
        {
            Debug.Log("Starting Game");

            Time.timeScale = 1f;
            playerTransform.position = spawnPoint.position;
            timer.StartTimer();
        }

        public void EndGame()
        {
            Debug.Log("GAME OVER");

            Time.timeScale = 0f;
            timer.PauseTimer();
            OpenShopUI();
        }

        public void RestartGame()
        {
            
            
            Debug.Log("Restarting game...");
            Time.timeScale = 1f;
            CloseShop();

            if (timer != null && health != null)
            {
                timer.RestartTimer();
                health.ResurrectPlayer();
            }
            
            // DELETE MOBS AND REPAWN THEM
        }
        
        #region SHOP
        
        private void OpenShopUI()
        {
            if (shopUI != null)
            {
                Debug.Log("OpeningShop");
                _currentTimer = timer.GetTimer();
                shopUI.OpenShop(_currentTimer);
            }
        }
        
        private void CloseShop()
        {
            if (shopUI != null)
            {
                _currentTimer = string.Empty;
                shopUI.RespawnButton();
            }
        }
        
        #endregion
    }
}