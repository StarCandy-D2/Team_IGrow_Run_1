using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  
    [Header("스테이지 설정")]
    public float stageInterval = 10f;
    public int stage = 1;

    private float elapsedTime = 0f;

    
    
    private float eelpasedTime = 0f;

    private int currentScore = 0;
    private int bestscore = 0;
    private float playTime = 0f;
    private bool isGameRunning = true;
    UIManager uiManager;
    ScoreManager scoreManager;
    ShopSceneManager shopSceneManager;
    

    public UIManager UIManager
    {
        get { return uiManager; }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        uiManager = FindObjectOfType<UIManager>();
    }
    void Update()
    {
        if (isGameRunning)
        {
            playTime += Time.deltaTime;
            UIManager.UpdatePlaytime(playTime); // 실시간으로 반영
        }
    }

    private void Start()
    {
        BestScore();
        uiManager.UpdateScore(0);
        uiManager.UpdateBestScore(PlayerPrefs.GetInt("Score_0", 0));
        uiManager.UpdateBigBestScore(PlayerPrefs.GetInt("Score_0", 0));
        //StartBGM 제거
        GameObject bgm = GameObject.Find("BGMPlayer");
        if (bgm != null)
        {
            Destroy(bgm);
        }

        // 새로운 음악 재생
        GetComponent<AudioSource>().Play();
    }
    

    public void GameOver()
    {
        Debug.Log("Game Over");
        ScoreManager.SaveScore(currentScore);
        PlayerPrefs.SetInt("LastScore", currentScore); // 대기화면에 띄울 점수 저장
        uiManager.SetRestart();
        isGameRunning = false;
        UIManager.SetRestart(); // 사망 시 재시작 UI 표시
        UIManager.UpdatePlaytime(playTime); // 종료 시 최종 시간 표시
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
