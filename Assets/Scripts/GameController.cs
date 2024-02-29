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

    [Header("Script References")]
    public ScoreManager scoreManager;
    [SerializeField] private HighscoreTable highscoreTable;

[Header("BGM")]
    [SerializeField] private AudioSource gameBGM;

    [Header("Internals")]
    private bool isPaused = false;
    private float waitTime = 1f;

    private void Start()
    {
        TogglePause();
        tutorialPanel.SetActive(true);
    }

    public void GoodEnd()
    {
        StartCoroutine(GoodEndCoroutine());
    }
    
    public void BadEnd()
    {
        StartCoroutine(BadEndCoroutine());
    }

    IEnumerator GoodEndCoroutine()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Pause();
        yield return new WaitForSecondsRealtime(endCooldown);
        gameBGM.Stop();
        highscoreTable.AddHighscoreEntry(scoreManager.currentScore);
        goodEndPanel.SetActive(true);
        goodFinalScore.text = scoreManager.currentScore.ToString();
    }

    IEnumerator BadEndCoroutine()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Pause();
        yield return new WaitForSecondsRealtime(endCooldown);
        gameBGM.Stop();
        highscoreTable.AddHighscoreEntry(scoreManager.currentScore);
        badEndPanel.SetActive(true);
        badFinalScore.text = scoreManager.currentScore.ToString();
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
