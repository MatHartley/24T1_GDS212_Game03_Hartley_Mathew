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

    [Header("Script References")]
    public ScoreManager scoreManager;

    [Header("BGM")]
    [SerializeField] private AudioSource gameBGM;

    [Header("Internals")]
    private bool isPaused = false;

    private void Start()
    {
        TogglePause();
        tutorialPanel.SetActive(true);
    }

    public void GoodEnd()
    {
        TogglePause();
        goodEndPanel.SetActive(true);
        goodFinalScore.text = scoreManager.currentScore.ToString();
    }

    public void BadEnd()
    {
        TogglePause();
        badEndPanel.SetActive(true);
        badFinalScore.text = scoreManager.currentScore.ToString();
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            //pause the game
            Time.timeScale = 0f;
        }
        else
        {
            //unpause the game
            Time.timeScale = 1f;
        }

        isPaused = !isPaused;
    }
}
