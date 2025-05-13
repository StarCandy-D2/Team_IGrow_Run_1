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
        // ����
        if (player.JumpCount == 1)
        {
            animator.SetTrigger("Jump1");
        }
        //else if (player.JumpCount == 2)
        //{
        //    animator.SetTrigger("Jump2");
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

        // ���� �� �޸���
        animator.SetBool("isGrounded", player.IsGrounded());

        // ����
        //if (player.isDead)
        //{
        //    animator.SetTrigger("Die");
        //}
    }
}
