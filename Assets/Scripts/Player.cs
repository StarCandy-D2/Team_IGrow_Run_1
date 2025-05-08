using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [Header("����/�̵�")]
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
            jumpCount = 0;  // ���鿡 ������ ���� Ƚ�� �ʱ�ȭ
            Debug.Log("���� ���� ����Ƚ�� �ʱ�ȭ");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("������");
        }
    }
}
