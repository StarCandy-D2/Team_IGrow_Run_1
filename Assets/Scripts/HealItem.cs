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
            Debug.Log($"���� �÷��̾��� ü���� {player.CurrentHp}");
            Debug.Log("ȸ���Ǿ����ϴ�!");
            player.CurrentHp += 2;
            Debug.Log($"���� �÷��̾��� ü���� {player.CurrentHp}");
            Destroy(this.gameObject);

        }
    }
}
