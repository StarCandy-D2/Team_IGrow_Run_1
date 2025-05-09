using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterItem : MonoBehaviour
{
    public MovingGround movingGround;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (movingGround != null)
        {
            
            transform.position = new Vector3(0, 10, 0); //�ش� ������Ʈ�� �ٷ� Destroy�ϸ� �ڷ�ƾ�� ���߹Ƿ� ���� ȿ���� �������� ������Ʈ�� �� ������ �̵� 
            StartCoroutine(movingGround.Booster()); // �ڷ�ƾ �Լ� ȣ��
            
        }
        else
        {
            Debug.LogError("MovingGround�� null�Դϴ�. ������ Ȯ���ϼ���.");
        }
        Destroy(this.gameObject, 3f);
    }
}
