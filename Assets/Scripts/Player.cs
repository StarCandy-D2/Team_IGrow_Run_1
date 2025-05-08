using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] int jumpCount = 0;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RunAndJump();
        Sliding();
    }

    void RunAndJump()
    {
        Vector2 vec = rigid.velocity;
        vec.x = runSpeed;

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
