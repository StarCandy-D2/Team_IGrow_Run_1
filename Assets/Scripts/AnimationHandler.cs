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

        // 점프
        if (player.JumpCount == 1 && wasGrounded)
        {
            animator.SetTrigger("Jump1");
        }
        else if (player.JumpCount == 2 && !wasGrounded)
        {
            animator.SetTrigger("Jump2");
        }

        // 착지 여부
        bool isGrounded = player.IsGrounded();
        animator.SetBool("isGrounded", isGrounded);

        // 하강 여부 감지
        float yVelocity = player.GetComponent<Rigidbody2D>().velocity.y;
        bool isFalling = yVelocity < 0 && !isGrounded;
        animator.SetBool("isFalling", isFalling);

        // 이전 프레임과 비교해서 착지 타이밍 판단
        if (!wasGrounded && isGrounded)
        {
            // 애니메이터 파라미터 설정
        }
        
        // 착지 감지
        wasGrounded = isGrounded;


        // 슬라이딩
        if (Input.GetKey(slideKey))
        {
            animator.SetBool("Slide", true);
        }
        else
        {
            animator.SetBool("Slide", false);
        }

        //죽음
        //if (player.isDead)
        //{
        //    animator.SetTrigger("Die");
        //}
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
