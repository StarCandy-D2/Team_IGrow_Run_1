using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    private static BGMPlayer instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �� ������Ʈ ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
        }
    }
    void OnEnable()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 100f);
        GetComponent<AudioSource>().volume = savedVolume / 100f;
    }
}

