using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneManager : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject BackButton;

    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void BackToStartUI()
    {
        SceneManager.LoadScene("StartUIScene");
    }
}
