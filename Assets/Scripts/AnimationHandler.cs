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
        // ���� Ʈ���� ����
        if (!wasGrounded && player.IsGrounded())
        {
            animator.SetTrigger("Land");
        }

        // ���� ������Ʈ
        wasGrounded = player.IsGrounded();

        // ����
        if (player.JumpCount == 1)
        {
            animator.SetTrigger("Jump1");
        }
        else if (player.JumpCount == 2)
        {
            animator.SetTrigger("Jump2");
        }

        // �����̵�
        if (Input.GetKey(slideKey))
        {
            animator.SetBool("Slide", true);
        }
        else
        {
            animator.SetBool("Slide", false);
        }

        // ���� �� �޸���
        animator.SetBool("isGrounded", player.IsGrounded());

        //����
        if (player.isDead)
        {
            animator.SetTrigger("Die");
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
