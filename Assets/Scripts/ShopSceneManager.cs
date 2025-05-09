using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int jellyprice = 1;
    public int maxHPprice = 1;
    public int lifeprice = 1;
    public int otherprice = 1;

    public int calcurateconst1 = 5;
    public int calcurateconst2 = 5;
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
}
