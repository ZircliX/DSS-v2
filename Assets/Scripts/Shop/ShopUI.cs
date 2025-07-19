using System.Collections;
using TMPro;
using UnityEngine;

namespace DSS.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private GameObject shopPanel;
        
        [SerializeField] private TMP_Text shopError;
        
        private string Error = "Not enough gold to upgrade !";
        
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