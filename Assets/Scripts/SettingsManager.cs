using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class SettingsManager : MonoBehaviour
{

    public Slider volumeSlider;
    public TMP_InputField volumeInputField;

    public TMP_InputField jumpKeyInputField;
    public TMP_InputField slideKeyInputField;

    private AudioSource bgmSource;
    private bool isEditingJump = false;
private bool isEditingSlide = false;

    void Start()
    {
        // 슬라이더 범위 강제 설정
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 100f;
        volumeSlider.wholeNumbers = true;

        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 100f);
        volumeSlider.value = savedVolume;
        volumeInputField.text = savedVolume.ToString("0") + "%";

        bgmSource = FindObjectOfType<BGMPlayer>()?.GetComponent<AudioSource>();
        if (bgmSource != null)
        {
            bgmSource.volume = savedVolume / 100f;
        }

        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
        volumeInputField.onEndEdit.AddListener(OnVolumeInputChanged);
    }


    public void OnSliderChanged(float value)
    {
        bgmSource.volume = value / 100f;
        volumeInputField.text = value.ToString("0") + "%";
        PlayerPrefs.SetFloat("BGMVolume", value);
        PlayerPrefs.Save();
    }

    public void OnVolumeInputChanged(string value)
    {
        value = value.Replace("%", "");
        if (float.TryParse(value, out float result))
        {
            result = Mathf.Clamp(result, 0f, 100f);
            volumeSlider.value = result;
        }

    }

    public void BacktoMain()
    {
        SceneManager.LoadScene("StartUIScene");
    }

}

