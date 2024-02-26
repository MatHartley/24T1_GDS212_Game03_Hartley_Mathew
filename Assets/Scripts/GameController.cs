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

    [Header("Script References")]
    public ScoreManager scoreManager;

    public void GoodEnd()
    {
        Pause();
        goodEndPanel.SetActive(true);
        goodFinalScore.text = scoreManager.currentScore.ToString();
    }

    public void BadEnd()
    {
        Pause();
        badEndPanel.SetActive(true);
        badFinalScore.text = scoreManager.currentScore.ToString();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
    }
}
