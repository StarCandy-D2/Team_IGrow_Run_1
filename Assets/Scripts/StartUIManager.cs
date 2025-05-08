using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour
{
    public GameObject scoreBoardPanel;
    public TextMeshProUGUI[] rankTexts;
    public GameObject creditPanel;
    public GameObject StartButton;
    public GameObject ScoreBoardButton;
    public GameObject CreditButton;
    public GameObject QuitButton;
    public GameObject HideScoreBoardButton;
    public GameObject HideCreditButton;
    public void ShowScoreBoard()
    {
        scoreBoardPanel.SetActive(true);
        for (int i = 0; i < rankTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt($"Score_{i}", 0);
            rankTexts[i].text = $"{i + 1}grade : {score}point";
        }
        HideScoreBoardButton.SetActive(true);
        StartButton.SetActive(false);
        ScoreBoardButton.SetActive(false);
        CreditButton.SetActive(false);
        QuitButton.SetActive(false);
    }

    public void HideScoreBoard()
    {
        scoreBoardPanel.SetActive(false);
        StartButton.SetActive(true);
        ScoreBoardButton.SetActive(true);
        CreditButton.SetActive(true);
        QuitButton.SetActive(true);
    }
    public void ShowCredits()
    {
        HideCreditButton.SetActive(true);
        creditPanel.SetActive(true);
        StartButton.SetActive(false);
        ScoreBoardButton.SetActive(false);
        CreditButton.SetActive(false);
        QuitButton.SetActive(false);
    }

    public void HideCredits()
    {
        creditPanel.SetActive(false);
        StartButton.SetActive(true);
        ScoreBoardButton.SetActive(true);
        CreditButton.SetActive(true);
        QuitButton.SetActive(true);
    }
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
    }
}
