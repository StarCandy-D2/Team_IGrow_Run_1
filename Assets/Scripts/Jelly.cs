using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public Animator animator;
    int score = 0;
    bool hasEaten = false;

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
        if (!hasEaten) // 트리거가 두번 작동하는 것을 방지하기위한 조건문
        {
            hasEaten = true;
            //Debug.Log(++score); // UI의 스코어랑 연동하여 사용
            //Debug.Log("젤리 먹기");
            animator.SetBool("IsEat", true);

            Destroy(this.gameObject, 0.2f);
        }

    }
}
