using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float jumpPower;
    [SerializeField] float BaseRunSpeed;
    private float currentRunSpeed;
    
    [SerializeField] int jumpCount = 0;

    public int stage = 0;
    [SerializeField] float stageInterval = 10f;     //stage time
    [SerializeField] float speedIncreasePerStage = 0.5f; // increase speed per stage ex)3stage =  +1.5f
    private float elapsedTime = 0f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        currentRunSpeed = BaseRunSpeed;
    }

    private void Update()
    {
        RunAndJump();
        Sliding();
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= stageInterval)
        {
            elapsedTime -= stageInterval;
            stage++;
            currentRunSpeed = BaseRunSpeed + stage * speedIncreasePerStage;  // ex 3stage  -> 3(base) + 3*0.5 = 4.5f
        }
    }

    void RunAndJump()
    {
        Vector2 vec = rigid.velocity;
        vec.x = currentRunSpeed;

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            if(jumpCount < 2)
            {
                jumpCount++;
                vec.y = jumpPower;
            }
        }

        rigid.velocity = vec;
    }

    void Sliding()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E))
        {
            Vector2 vec = rigid.velocity;
            vec.y = -jumpPower;
            rigid.velocity = vec;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            jumpCount = 0;
        }
    }
}
