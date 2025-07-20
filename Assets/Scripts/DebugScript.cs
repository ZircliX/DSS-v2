using System;
using UnityEngine;

namespace DSS
{
    public class DebugScript : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                GameManager.Instance.EndGame();
            }
        }
    }
}