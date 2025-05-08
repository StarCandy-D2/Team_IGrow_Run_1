using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [Header("점프/이동")]
    public float jumpForce = 5f;
    public float forwardSpeed = 4f;
    public bool isDead = false;
    public bool isGrounded = false;
    bool isJump = false;
    public bool godMode = false;
    private int jumpCount = 0;
    private const int maxJumpCount = 2;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody != null)
            Debug.LogError("rigidbody error");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&jumpCount<maxJumpCount)
        {
         isJump = true;
         jumpCount++;
        }
        
    }
    public void FixedUpdate()
    {
        if (isDead) return;

        Vector2 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (isJump)
        {
            velocity.y = 0;
            velocity.y += jumpForce;
            isJump = false;
        }
        _rigidbody.velocity = velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;  // 지면에 닿으면 점프 횟수 초기화
            Debug.Log("땅에 닿음 점프횟수 초기화");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("점프함");
        }
    }
}
