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
            
            transform.position = new Vector3(0, 10, 0); //해당 오브젝트를 바로 Destroy하면 코루틴이 멈추므로 먹은 효과를 내기위해 오브젝트를 맵 밖으로 이동 
            StartCoroutine(movingGround.Booster()); // 코루틴 함수 호출
            
        }
        else
        {
            Debug.LogError("MovingGround가 null입니다. 연결을 확인하세요.");
        }
        Destroy(this.gameObject, 3f);
    }
}
