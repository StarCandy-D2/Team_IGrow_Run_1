using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI BestScore;
    public GameObject restartPanel;
    public Button restartButton;
    public Button backToMainButton;
    public TextMeshProUGUI BigScoreText;
    public TextMeshProUGUI BigBestScore;
    public void Start()
    {
        if (restartPanel == null)
        {
            Debug.LogError("restart text is null");
        }

        if (scoreText == null)
        {
            Debug.LogError("scoreText is null");
            return;
        }
        if (restartButton == null)
        {
            Debug.LogError("restart button is null");
        }
        if (BestScore == null)
        {
            Debug.LogError("BestScore == null");
        }
        if (backToMainButton == null)
        {
            Debug.LogError("backToMainButton == null");
        }
        if (BigScoreText == null)
        {
            Debug.LogError("BigScoreText == null");
        }
        if(BigBestScore == null)
        {
            Debug.LogError("BigBestScore == null");
        }
        restartPanel.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        BigBestScore.gameObject.SetActive(false);
        BigScoreText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        restartPanel.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        backToMainButton.gameObject.SetActive(true);
        BigScoreText.gameObject.SetActive(true);
        BigBestScore.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        BestScore.gameObject.SetActive(false);
    }


    public void UpdateScore(int score)
    {
        scoreText.text = "Score    " + score.ToString();
    }
    public void UpdateBestScore(int bestscore)
    {
        if (BestScore != null)
        BestScore.text = "Best Score    " + bestscore.ToString();
    }    
    public void UpdateBigScore(int score)
    {
        BigScoreText.text = "Score  " + score.ToString();
    }
    public void UpdateBigBestScore(int bestscore)
    {
        if (BestScore != null)
        BigBestScore.text = "Best Score  " + bestscore.ToString();
    }
    public void ReturnToMainScene()
    {
        SceneManager.LoadScene("LobbyScene"); // 메인씬 이름 정확히!
    }


}
