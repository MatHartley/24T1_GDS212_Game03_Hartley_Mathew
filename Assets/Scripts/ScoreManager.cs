using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score")]
    public int currentScore;

    [Header("Credit")]
    public int currentCredit;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI creditText;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        currentCredit = 0;
        scoreText.text = currentScore.ToString();
        creditText.text = currentCredit.ToString();
    }

    public void UpdateScore(int addition)
    {
        currentScore += addition;
        scoreText.text = currentScore.ToString();
    }

    public void UpdateCredit(int addition)
    {
        currentCredit += addition;
        creditText.text = currentCredit.ToString();
    }
}
