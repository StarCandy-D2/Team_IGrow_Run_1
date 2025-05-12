using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static void SaveScore(int score)
    {
        // ���� ������ �ҷ�����
        List<int> scoreList = new();
        for (int i = 0; i < 5; i++)
        {
            scoreList.Add(PlayerPrefs.GetInt($"Score_{i}", 0));
        }

        // ���ο� ���� �߰��ϰ� ����
        scoreList.Add(score);
        scoreList.Sort((a, b) => b.CompareTo(a)); // ��������
        if (scoreList.Count > 5)
            scoreList.RemoveAt(5);

        // �ٽ� ����
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt($"Score_{i}", scoreList[i]);
        }
        PlayerPrefs.Save();
    }
}
