using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DSS.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private GameObject shopPanel;
        
        [SerializeField] private TMP_Text shopError;
        [SerializeField] private TMP_Text timerText;
        private string Error = "Not enough gold to upgrade !";

        public void OpenShop(string timerToShow)
        {
            shopPanel.SetActive(true);
            timerText.text = $"YOU DIED IN {timerToShow}";
        }

        public void RespawnButton()
        {
            shopPanel.SetActive(false);
        }
        
        #region ErrorToUpgrade
        public void DoErrorToUpgrade()
        {
            StartCoroutine(ShowErrorCoroutine());
        }

        private IEnumerator ShowErrorCoroutine()
        {
            shopError.text = string.Empty;
            shopError.text = Error;
            yield return new WaitForSeconds(1f);
            shopError.text = string.Empty;
        }
        #endregion
    }
}