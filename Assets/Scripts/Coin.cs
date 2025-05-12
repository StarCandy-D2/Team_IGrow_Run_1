using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Player player;
    bool isEat = false;
    
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEat)
        {
            isEat = true;
            if (gameObject.CompareTag("SilverCoin"))
            {
                player.coins += 100;
                Debug.Log("Ω«πˆƒ⁄¿Œ »πµÊ!");
            }
            else if (gameObject.CompareTag("GoldCoin"))
            {
                player.coins += 1000;
                Debug.Log("∞ÒµÂƒ⁄¿Œ »πµÊ!");
            }
            Destroy(this.gameObject);
        }
    }
}
