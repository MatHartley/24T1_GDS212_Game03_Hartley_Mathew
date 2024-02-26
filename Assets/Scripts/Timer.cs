using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MathewHartley
{
    public class Timer : MonoBehaviour
    {
        [Header("--- Component ---")]
        public TextMeshProUGUI timerText;

        [Header("--- Timer Settings ---")]
        public float currentTime;
        public bool countDown = false;

        [Header("--- Limit Settings ---")]
        public bool hasLimit = false;
        public float timerLimit;

        [Header("--- Control Settings ---")]
        public bool isPaused = false;

        [Header("--- Format Settings ---")]
        public bool hasFormat = true;
        public TimerFormats format;
        private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

        // Start is called before the first frame update
        void Start()
        {
            timeFormats.Add(TimerFormats.Whole, "0");
            timeFormats.Add(TimerFormats.TenthsDecimal, "0.0");
            timeFormats.Add(TimerFormats.HundredthsDecimal, "0.00");
            timeFormats.Add(TimerFormats.MinuteSecond, "0:00");
        }

        // Update is called once per frame
        void Update()
        {
            //checks if the timer is paused and stops increasing or decreasing if true
            if (!isPaused)
            {
                //makes timer count down is countDown bool is true, and count up if countDown bool is false
                if (countDown)
                {
                    currentTime -= Time.deltaTime;
                }
                else
                {
                    currentTime += Time.deltaTime;
                }

                //checks if a set time limit has been reached by either counting up or down locking the timer at the limit and changing the colour to red if true
                if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
                {
                    currentTime = timerLimit;
                    SetTimerText();
                    timerText.color = Color.red;
                    enabled = false;
                }
            }
            SetTimerText();

        }
        private void SetTimerText()
        {
            // outputs time to text box assigned to text component
            switch (format)
            {
                default: 
                    timerText.text = currentTime.ToString(); 
                    break;
                case TimerFormats.Whole: 
                    timerText.text = currentTime.ToString(timeFormats[format]); 
                    break;
                case TimerFormats.TenthsDecimal: 
                    timerText.text = currentTime.ToString(timeFormats[format]); 
                    break;
                case TimerFormats.HundredthsDecimal: 
                    timerText.text = currentTime.ToString(timeFormats[format]); 
                    break;
                case TimerFormats.MinuteSecond:
                    int minutes = Mathf.FloorToInt(currentTime / 60);
                    int seconds = Mathf.FloorToInt(currentTime % 60);
                    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
                    break;
            }
        }

        public enum TimerFormats
        {
            Whole,
            TenthsDecimal,
            HundredthsDecimal,
            MinuteSecond,
        }
    }
}