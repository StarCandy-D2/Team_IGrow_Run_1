using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float flashDuration = 0.15f;
    private Animator animator;
    private Player player;
    private Color originalColor;
    private KeyCode jumpKey;
    private KeyCode slideKey;
    private bool wasGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        originalColor = spriteRenderer.color;
        jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpKey", "Space"));
        slideKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SlideKey", "LeftShift"));
    }

    void Update()
    {

        // ����
        if (player.JumpCount == 1 && wasGrounded)
        {
            animator.SetTrigger("Jump1");
        }
        else if (player.JumpCount == 2 && !wasGrounded)
        {
            animator.SetTrigger("Jump2");
        }

        // ���� ����
        bool isGrounded = player.IsGrounded();
        animator.SetBool("isGrounded", isGrounded);

        // �ϰ� ���� ����
        float yVelocity = player.GetComponent<Rigidbody2D>().velocity.y;
        bool isFalling = yVelocity < 0 && !isGrounded;
        animator.SetBool("isFalling", isFalling);

        // ���� �����Ӱ� ���ؼ� ���� Ÿ�̹� �Ǵ�
        if (!wasGrounded && isGrounded)
        {
            // �ִϸ����� �Ķ���� ����
        }
        
        // ���� ����
        wasGrounded = isGrounded;


        // �����̵�
        if (Input.GetKey(slideKey))
        {
            animator.SetBool("Slide", true);
        }
        else
        {
            animator.SetBool("Slide", false);
        }

        //����
        //if (player.isDead)
        //{
        //    animator.SetTrigger("Die");
        //}
    }

    // ������ ������ ���� ����
    public void FlashDamageColor()
    {
        StopAllCoroutines();
        StartCoroutine(FlashColor());
    }

    private IEnumerator FlashColor()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}
