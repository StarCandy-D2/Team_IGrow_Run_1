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
        // �Ǵ� ���� �����ο� �°� ��� ��� ����
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // MainScene������ ��Ȱ��ȭ
        if (scene.name == "ShopScene")
        {
            gameObject.SetActive(true);  // shop������ ���̰�
        }
        else
        {
            gameObject.SetActive(false);   // ���������� ����
        }
    }
}
