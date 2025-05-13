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
    [SerializeField] private JellyLevelData jellyLevelData;
    [SerializeField] private Image jellyLevelImage;

    public int jellyprice = 1;
    public int maxHPprice = 1;
    public int lifeprice = 1;
    public int otherprice = 1;

    public int calcurateconst1 = 5;
    public int calcurateconst2 = 5;
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
        recentCoin.text = $"Coin : {ShopPlayer.Instance.coins}";
    }

    public void UpgradeJelly()
    {
        int cost = ShopPlayer.Instance.GetUpgradeCost(ShopPlayer.Instance.jellyLevel);
        if (ShopPlayer.Instance.coins >= cost)
        {
            ShopPlayer.Instance.coins -= cost;
            ShopPlayer.Instance.jellyLevel++;
            Debug.Log("젤리 업그레이드!");
            UpdateCoinUI();
            UpdateUpgradePriceUI();
            UpdateJellyLevelImage();
        }
        else
        {
            Debug.Log("코인이 부족합니다.");
        }
    }
    public void UpgradeMaxHP()
    {
        if (ShopPlayer.Instance.TryUpgrade(ref ShopPlayer.Instance.maxHPLevel))
        {
            Debug.Log("체력 업그레이드!");
        }
        else Debug.Log("코인이 부족합니다.");
        UpdateCoinUI();
        UpdateUpgradePriceUI();
    }
    public void Upgradelife()
    {
        if (ShopPlayer.Instance.TryUpgrade(ref ShopPlayer.Instance.lifeLevel))
        {
            Debug.Log("회복아이템 업그레이드!");
        }
        else Debug.Log("코인이 부족합니다.");
        UpdateCoinUI();
        UpdateUpgradePriceUI();
    }
    public void UpgradeOther()
    {
        if (ShopPlayer.Instance.TryUpgrade(ref ShopPlayer.Instance.otherLevel))
        {
            Debug.Log("기타아이템 업그레이드!");
        }
        else Debug.Log("코인이 부족합니다.");
        UpdateCoinUI();
        UpdateUpgradePriceUI();
    }
    public void UpdateUpgradePriceUI()
    {
        jellytext.text = $"Price: {ShopPlayer.Instance.GetUpgradeCost(ShopPlayer.Instance.jellyLevel)}";
        maxHPtext.text = $"Price: {ShopPlayer.Instance.GetUpgradeCost(ShopPlayer.Instance.maxHPLevel)}";
        lifetext.text = $"Price: {ShopPlayer.Instance.GetUpgradeCost(ShopPlayer.Instance.lifeLevel)}";
        othertext.text = $"Price: {ShopPlayer.Instance.GetUpgradeCost(ShopPlayer.Instance.otherLevel)}";
    }
    private void UpdateJellyLevelImage()
    {
        int jellyLevel = Mathf.Clamp(ShopPlayer.Instance.jellyLevel, 1, jellyLevelData.levelSprites.Count);
        jellyLevelImage.sprite = jellyLevelData.levelSprites[jellyLevel - 1];
    }

    void Start()
    {
        UpdateUpgradePriceUI();
        UpdateCoinUI(); // 이 부분도 ShopPlayer.Instance.coins 기준으로 수정돼야 함
        UpdateJellyLevelImage();
    }
    public void TestClick()
    {
        Debug.Log("버튼 눌림!");
    }

}
