using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    public Player player;
    float movespeed = 5f;
    float origineSpeed;
    bool isBoost = false;

    // Start is called before the first frame update
    void Start()
    {
        origineSpeed = movespeed;
    }

    // Update is called once per frame
    void Update()
    {

        
        Vector3 pos = transform.position;
        pos.x -= movespeed * Time.deltaTime;
        transform.position = pos;
        if (player.isDead == true)
        {
            movespeed = 0;
        }
    }

    public IEnumerator Booster() // �ν��� �ڷ�ƾ �Լ�
    {
        if (isBoost) yield break; // Ʈ���� �ߺ� �ߵ��������� �ߺ� ȣ�� ����

        Debug.Log("��������.");
        movespeed = 15f;
        isBoost = true;
        player.isGod = true;
        yield return new WaitForSeconds(2f); //�ش� �Լ��� 2�� ����  
        
        Debug.Log("2����");
        movespeed = origineSpeed; // ���� ���ǵ�� ����
        Debug.Log("���ƿԴ�.");
        isBoost = false;
        player.isGod = false;


    }
}
