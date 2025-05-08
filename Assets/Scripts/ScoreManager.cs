using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static void SaveScore(int score)
    {
        // 기존 점수들 불러오기
        List<int> scoreList = new();
        for (int i = 0; i < 10; i++)
        {
            scoreList.Add(PlayerPrefs.GetInt($"Score_{i}", 0));
        }

        // 새로운 점수 추가하고 정렬
        scoreList.Add(score);
        scoreList.Sort((a, b) => b.CompareTo(a)); // 내림차순
        if (scoreList.Count > 10)
            scoreList.RemoveAt(10);

        // 다시 저장
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt($"Score_{i}", scoreList[i]);
        }
        PlayerPrefs.Save();
    }
}
