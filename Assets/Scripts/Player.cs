using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpPower;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RunAndJump();
    }

    void RunAndJump()
    {
        Vector2 vec = rigid.velocity;
        vec.x = runSpeed;

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            vec.y = jumpPower;
        }

        rigid.velocity = vec;
    }
}
