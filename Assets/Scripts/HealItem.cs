using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public Player player;
    bool isEat = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEat)
        {
            isEat = true;
            Debug.Log($"현재 플레이어의 체력은 {player.CurrentHp}");
            Debug.Log("회복되었습니다!");
            player.CurrentHp += 2;
            Debug.Log($"현재 플레이어의 체력은 {player.CurrentHp}");
            Destroy(this.gameObject);

        }
    }
}
