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
    bool jump1Triggered = false;
    bool jump2Triggered = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        // ����
        if (player.JumpCount == 1)
        {
            animator.SetTrigger("Jump1");
            jump1Triggered = true;
        }
        //else if (player.JumpCount == 2)
        //{
        //    animator.SetTrigger("Jump2");
        //    jump2Triggered = true;
        //}

        // �����̵�
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.E))
        {
            animator.SetBool("Slide", true);
        }
        else
        {
            animator.SetBool("Slide", false);
        }

        if (player.IsGrounded())
        {
            jump1Triggered = false;
            jump2Triggered = false;
        }

        // ���� �� �޸���
        animator.SetBool("isGrounded", player.IsGrounded());

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
