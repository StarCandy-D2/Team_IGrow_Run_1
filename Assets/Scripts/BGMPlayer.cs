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
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 이 오브젝트 유지
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }
    void OnEnable()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 100f);
        GetComponent<AudioSource>().volume = savedVolume / 100f;
    }
}

