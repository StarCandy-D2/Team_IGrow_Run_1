using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int currentScore = 0;

    public void AddScore(int score)
    {
        currentScore += score;

        Debug.Log("Score: " + currentScore);
    }
}
