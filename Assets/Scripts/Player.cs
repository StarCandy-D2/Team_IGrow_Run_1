using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider;                  //추후 콜라이더에 따라 수정
    private Vector2 offsetVec = new Vector2(0, -0.25f); //추후 콜라이더에 따라 수정
    private Vector2 sizeVec = new Vector2(1, 0.5f);     //추후 콜라이더에 따라 수정
    [SerializeField] float runSpeed;
    [SerializeField] float jumpPower;
    private int jumpCount = 0;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
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
            if (jumpCount < 2)
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
            boxCollider.offset = offsetVec;     //추후 콜라이더에 따라 수정
            boxCollider.size = sizeVec;         //추후 콜라이더에 따라 수정
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            jumpCount = 0;
        }
    }
}
