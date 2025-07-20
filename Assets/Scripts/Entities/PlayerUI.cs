using TMPro;
using UnityEngine;

namespace DSS.Entities
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text money;

        private void Update()
        {
            money.text = Player.Instance.Gold.ToString();
        }
    }
}