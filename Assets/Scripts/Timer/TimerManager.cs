using System;
using LTX.Singletons;
using TMPro;
using UnityEngine;

namespace DSS.Timer
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        
        private float currentTime = 0f;
        private bool isRunning = false;
        
        public float CurrentTime => currentTime;
        public bool IsRunning => isRunning;
        


        private void Update()
        {
            if (isRunning)
            {
                currentTime += Time.deltaTime;
                UpdateTimerDisplay();
            }
        }

        public void StartTimer()
        {
            isRunning = true;
        }

        public void PauseTimer()
        {
            isRunning = false;
            
        }

        public void ResetTimer()
        {
            currentTime = 0f;
        }

        public void RestartTimer()
        {
            ResetTimer();
            StartTimer();
        }
        
        private void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }

        public string GetTimer()
        {
            return timerText.text;
        }
    }
}