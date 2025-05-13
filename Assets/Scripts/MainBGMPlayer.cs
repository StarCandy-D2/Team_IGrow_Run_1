using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGMPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("BGMVolume", 100f);
        GetComponent<AudioSource>().volume = volume / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
