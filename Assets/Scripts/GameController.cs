using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject goodEndPanel;
    [SerializeField] private TextMeshProUGUI goodFinalScore;
    [SerializeField] private GameObject badEndPanel;
    [SerializeField] private TextMeshProUGUI badFinalScore;
    [SerializeField] private GameObject tutorialPanel;

    [Header("Countdowns")]
    [SerializeField] private float endCooldown;
    [SerializeField] private float endCooldownCount;
    private bool countdownStart;

    [Header("Script References")]
    public ScoreManager scoreManager;

    [Header("BGM")]
    [SerializeField] private AudioSource gameBGM;

    [Header("Internals")]
    private bool isPaused = false;
    private bool waitToPause;
    private float waitTime = 1f;

    private void Start()
    {
        TogglePause();
        tutorialPanel.SetActive(true);

        endCooldownCount = endCooldown;
    }

    private void Update()
    {
        if (countdownStart)
        {
            endCooldownCount -= Time.unscaledDeltaTime;
        }
        if (waitToPause)
        {
            waitTime -= Time.deltaTime;
        }
    }

    public void GoodEnd()
    {
        waitToPause = true;
        if (waitTime <= 0)
        {
            Pause();
            countdownStart = true;
            gameBGM.Stop();
        }
        if (endCooldownCount <= 0)
        {
            goodEndPanel.SetActive(true);
            goodFinalScore.text = (scoreManager.currentScore + scoreManager.bonusScore).ToString();
        }
    }

    public void BadEnd()
    {
        waitToPause = true;
        if (waitTime <= 0)
        {
            Pause();
            countdownStart = true;
            gameBGM.Stop();
        }
        if (endCooldownCount <= 0)
        {
            badEndPanel.SetActive(true);
            badFinalScore.text = scoreManager.currentScore.ToString();
        }
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            //pause the game
            Time.timeScale = 0f;
            gameBGM.Pause();
        }
        else
        {
            //unpause the game
            Time.timeScale = 1f;
            gameBGM.UnPause();
        }

        isPaused = !isPaused;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameBGM.Pause();
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        gameBGM.UnPause();
    }
}
