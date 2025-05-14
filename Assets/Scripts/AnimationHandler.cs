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
        // 착지 트리거 감지
        if (!wasGrounded && player.IsGrounded())
        {
            animator.SetTrigger("Land");
        }

        // 상태 업데이트
        wasGrounded = player.IsGrounded();

        // 점프
        if (player.JumpCount == 1)
        {
            animator.SetTrigger("Jump1");
        }
        else if (player.JumpCount == 2)
        {
            animator.SetTrigger("Jump2");
        }

        // 슬라이딩
        if (Input.GetKey(slideKey))
        {
            animator.SetBool("Slide", true);
        }
        else
        {
            animator.SetBool("Slide", false);
        }

        // 착지 시 달리기
        animator.SetBool("isGrounded", player.IsGrounded());

        //죽음
        if (player.isDead)
        {
            animator.SetTrigger("Die");
        }
    }

    // 데미지 입을시 색상 변경
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
