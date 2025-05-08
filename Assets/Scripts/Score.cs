using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    Animator animator = null;
    Rigidbody2D _rigidbody = null;

    private int currentScore = 0;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isJump = false;
    GameManager gameManager = null;
    public void AddScore(int score)
    {
        currentScore += score;

        Debug.Log("Score: " + currentScore);
    }
    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    // 게임 재시작
                    gameManager.RestartGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isJump = true;
            }
        }
    }
}
