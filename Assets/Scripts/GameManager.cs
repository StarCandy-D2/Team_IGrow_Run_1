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
    UIManager uiManager;
    ScoreManager scoreManager;
    ShopSceneManager shopSceneManager;
    public int stage = 1;
    public int stageInterval = 1;

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
        //StartBGM ����
        GameObject bgm = GameObject.Find("BGMPlayer");
        if (bgm != null)
        {
            Destroy(bgm);
        }

        // ���ο� ���� ���
        GetComponent<AudioSource>().Play();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        ScoreManager.SaveScore(currentScore);
        PlayerPrefs.SetInt("LastScore", currentScore); // ���ȭ�鿡 ��� ���� ����
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
