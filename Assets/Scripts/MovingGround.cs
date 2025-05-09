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

    public IEnumerator Booster() // 부스터 코루틴 함수
    {
        if (isBoost) yield break; // 트리거 중복 발동으로인한 중복 호출 방지

        Debug.Log("빨라진다.");
        movespeed = 15f;
        isBoost = true;
        player.isGod = true;
        yield return new WaitForSeconds(2f); //해당 함수만 2초 정지  
        
        Debug.Log("2초후");
        movespeed = origineSpeed; // 원래 스피드로 복귀
        Debug.Log("돌아왔다.");
        isBoost = false;
        player.isGod = false;


    }
}
