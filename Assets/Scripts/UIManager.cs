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
    public GameObject restartPanel;
    public Button restartButton;
    public Button backToMainButton;
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
        restartPanel.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        restartPanel.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        backToMainButton.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void ReturnToMainScene()
    {
        SceneManager.LoadScene("StartUIScene"); // 메인씬 이름 정확히!
    }


}
