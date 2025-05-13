using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopPlayer : MonoBehaviour
{
    public static ShopPlayer Instance { get; private set; }

    public int jellyLevel = 1;
    public int maxHPLevel = 1;
    public int lifeLevel = 1;
    public int otherLevel = 1;
    public int coins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int GetUpgradeCost(int level) => 100 * level * level + 900;

    public bool TryUpgrade(ref int level)
    {
        int cost = GetUpgradeCost(level);
        if (coins >= cost)
        {
            coins -= cost;
            level++;
            return true;
        }
        return false;
    }
    public int GetMaxHP()
    {
        return 5 + (maxHPLevel -1 ) * 2;
        // 또는 게임 디자인에 맞게 계산 방식 변경
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // MainScene에서는 비활성화
        if (scene.name == "ShopScene")
        {
            gameObject.SetActive(true);  // shop씬에서 보이게
        }
        else
        {
            gameObject.SetActive(false);   // 나머지에서 감춤
        }
    }
}
