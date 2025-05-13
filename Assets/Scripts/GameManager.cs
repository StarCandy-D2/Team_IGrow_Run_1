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

    public float stageInterval = 10f;
    public int stage = 1;
    private float eelpasedTime = 0f;

    private int currentScore = 0;
    private int bestscore = 0;
    private float playTime = 0f;
    private bool isGameRunning = true;

    TutorialManager tutorialManager;
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
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
    void Update()
    {
        if (isGameRunning && !tutorialManager.isFirstRun)
        {
            playTime += Time.deltaTime;
            UIManager.UpdatePlaytime(playTime); // �ǽð����� �ݿ�
        }
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
        isGameRunning = false;
        UIManager.SetRestart(); // ��� �� ����� UI ǥ��
        UIManager.UpdatePlaytime(playTime); // ���� �� ���� �ð� ǥ��
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
