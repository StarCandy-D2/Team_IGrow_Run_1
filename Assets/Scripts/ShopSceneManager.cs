using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSceneManager : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject BackButton;
    public GameObject jellyButton;
    public GameObject maxHPButton;
    public GameObject lifeButton;
    public GameObject otherButton;
    public TextMeshProUGUI jellytext;
    public TextMeshProUGUI maxHPtext;
    public TextMeshProUGUI lifetext;
    public TextMeshProUGUI othertext;
    public GameObject jellytooltip;
    public GameObject maxhptooltip;
    public GameObject lifeitemtooltip;
    public GameObject otheritemtooltip;
    public TextMeshProUGUI jellytooltiptxt;
    public TextMeshProUGUI maxhptooltiptxt;
    public TextMeshProUGUI lifeitemtooltiptxt;
    public TextMeshProUGUI otheritemtooltiptxt;
    public TextMeshProUGUI recentCoin;
    [Header("Jelly Level UI")]
    [SerializeField] private Image jellyLevelImage;
    [SerializeField] private List<Sprite> jellyLevelSprites;

    public int jellyprice = 1;
    public int maxHPprice = 1;
    public int lifeprice = 1;
    public int otherprice = 1;

    public int calcurateconst1 = 5;
    public int calcurateconst2 = 5;
    public Player player;

   
    int GetUpgradeCost(int level)
    {
        return 100 * level * level + 900;
    }
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void BackToStartUI()
    {
        SceneManager.LoadScene("StartUIScene");
    }
    public void Showjelly()
    {
        jellytooltip.SetActive(true);
        jellytooltiptxt.text = "This jelly is made by crushing domestically grown fruits to extract their juice, then freezing it at sub-zero temperatures atop the Alps. It boasts a refreshingly cool yet delightfully chewy texture.";
    }
    public void Hidejelly()
    {
        jellytooltip.SetActive(false);
    }
    public void ShowMaxHP()
    {
        maxhptooltip.SetActive(true);
        maxhptooltiptxt.text = "This button lets the cookies get hit once for 10 coin each, helping them build up their toughness.";
    }
    public void HideMaxHP()
    {
        maxhptooltip.SetActive(false);
    }
    public void Showlife()
    {
        lifeitemtooltip.SetActive(true);
        lifeitemtooltiptxt.text = "Good medicine tastes bitter, they say. This button adds one extra herbal ingredient to the health potion, effectively tormenting the cookies for their own good.";
    }
    public void Hidelife()
    {
        lifeitemtooltip.SetActive(false);
    }
    public void Showother()
    {
        otheritemtooltip.SetActive(true);
        otheritemtooltiptxt.text = "The cookies will move faster and stronger. Of course... the whipping is done from behind.";
    }
    public void Hideother()
    {
        otheritemtooltip.SetActive(false);
    }
    public void UpdateCoinUI()
    {
        recentCoin.text = "coin : " + player.coins.ToString();
        if (player.coins == 0 )
        {
            recentCoin.text = "Coin : 0";
        }
    }
    public void UpgradeJelly()
    {
        int cost = GetUpgradeCost(player.jellylevel);
        if (player.coins >= cost)
        {
            player.coins -= cost;
            player.jellylevel++;
            Debug.Log("젤리 업그레이드!");
            UpdateCoinUI(); // ← 코인 UI 즉시 갱신
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }
    public void UpgradeMaxHP()
    {
        int cost = GetUpgradeCost(player.maxHPlevel);
        if (player.coins >= cost)
        {
            player.coins -= cost;
            player.maxHPlevel++;
            Debug.Log("최대 HP 업그레이드!");
            UpdateCoinUI(); // ← 코인 UI 즉시 갱신
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }
    public void Upgradelife()
    {
        int cost = GetUpgradeCost(player.lifelevel);
        if (player.coins >= cost)
        {
            player.coins -= cost;
            player.lifelevel++;
            Debug.Log("회북물약 업그레이드!");
            UpdateCoinUI(); // ← 코인 UI 즉시 갱신
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }
    public void UpgradeOther()
    {
        int cost = GetUpgradeCost(player.otherlevel);
        if (player.coins >= cost)
        {
            player.coins -= cost;
            player.otherlevel++;
            Debug.Log("기타 아이템 업그레이드!");
            UpdateCoinUI(); // ← 코인 UI 즉시 갱신
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }
    public void UpdateUpgradePriceUI()
    {
        jellytext.text = $"Price: {GetUpgradeCost(player.jellylevel)}";
        maxHPtext.text = $"Price: {GetUpgradeCost(player.maxHPlevel)}";
        lifetext.text = $"Price: {GetUpgradeCost(player.lifelevel)}";
        othertext.text = $"Price: {GetUpgradeCost(player.otherlevel)}";
    }
    private void UpdateJellyLevelImage()
    {
        int lvl = Mathf.Clamp(player.jellylevel, 1, jellyLevelSprites.Count);
        jellyLevelImage.sprite = jellyLevelSprites[lvl - 1];
    }
   
    void Start()
    {
        UpdateUpgradePriceUI();
        UpdateCoinUI();
        UpdateJellyLevelImage();
    }
}
