using UnityEngine;

namespace DSS.Menu
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private GameObject creditsPanel;
        [SerializeField] private GameObject mainMenuPanel;
        
        public void OpenCredits()
        {
            creditsPanel.SetActive(true);
        }

        public void CloseCredits()
        {
            creditsPanel.SetActive(false);

        }

        public void StartGame()
        {
            GameManager.Instance.StartGame();
            mainMenuPanel.SetActive(false);
        }
    }
}