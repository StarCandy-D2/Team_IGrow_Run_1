using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;
    private Player player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        // 점프
        if (player.JumpCount == 1)
        {
            animator.SetTrigger("Jump1");
        }
        //else if (player.JumpCount == 2)
        //{
        //    animator.SetTrigger("Jump2");
        //}

        // 슬라이딩
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.E))
        {
            animator.SetBool("Slide", true);
        }
        else
        {
            animator.SetBool("Slide", false);
        }

        // 착지 시 달리기
        animator.SetBool("isGrounded", player.IsGrounded());

        // 죽음
        //if (player.isDead)
        //{
        //    animator.SetTrigger("Die");
        //}
    }
}
