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
            Debug.Log("ȸ���Ǿ����ϴ�!");
            player.currentHp += 2;

            Destroy(this.gameObject);

        }
    }
}
