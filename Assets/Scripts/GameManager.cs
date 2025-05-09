using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public static GameManager Instance
    {
        get { return gameManager; }
    }

    private int currentScore = 0;
    private int bestscore = 0;
    public int stage = 1;
    UIManager uiManager;
    ScoreManager scoreManager;
    ShopSceneManager shopSceneManager;

    public UIManager UIManager
    {
        get { return uiManager; }
    }
    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        BestScore();
        uiManager.UpdateScore(0);
        uiManager.UpdateBestScore(PlayerPrefs.GetInt("Score_0", 0));
        uiManager.UpdateBigBestScore(PlayerPrefs.GetInt("Score_0", 0));
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        ScoreManager.SaveScore(currentScore);
        PlayerPrefs.SetInt("LastScore", currentScore); // 대기화면에 띄울 점수 저장
        uiManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);
        uiManager.UpdateBigScore(currentScore);
        Debug.Log("Score: " + currentScore);
    }
    public void BestScore()
    {
        bestscore = PlayerPrefs.GetInt("Score_0", 0);
        Debug.Log("Best Score: " + bestscore);
    }

}
