using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI bestScoreText;

    private void Start()
    {
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        lastScoreText.text = $"�̹� ����: {lastScore}";
        bestScoreText.text = $"�ְ� ����: {bestScore}";
    }
}
