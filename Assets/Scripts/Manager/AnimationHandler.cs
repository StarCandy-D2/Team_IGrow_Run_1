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

        // ���� ���� �Ǵ�
        bool isGrounded = player.IsGrounded();
        float yVelocity = player.GetComponent<Rigidbody2D>().velocity.y;

        bool isFalling = player.VerticalSpeed < -0.1f && !isGrounded;

        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isGrounded", isGrounded);

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
        if (player.isDead)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Cookie_Die"))
            {
                animator.SetTrigger("Die");
            }
        }
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
