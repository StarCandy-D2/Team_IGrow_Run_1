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

    void Start()
    {
        // 슬라이더 범위 강제 설정
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 100f;
        volumeSlider.wholeNumbers = true;

        // 초기화
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 100f);

        //0~1 범위의 잘못 저장된 값이면 → 0~100 기준으로 변환
        if (savedVolume <= 1f)
        {
            savedVolume *= 100f;
        }

        volumeSlider.value = savedVolume;
        volumeInputField.text = savedVolume.ToString("0") + "%";
        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
        volumeInputField.onEndEdit.AddListener(OnVolumeInputChanged);
        bgmSource = FindObjectOfType<BGMPlayer>()?.GetComponent<AudioSource>();
        if (bgmSource != null)
        {
            bgmSource.volume = savedVolume / 100f;
        }

        jumpKeyInputField.text = PlayerPrefs.GetString("JumpKey", "Space");
        slideKeyInputField.text = PlayerPrefs.GetString("SlideKey", "LeftShift");
        jumpKeyInputField.onEndEdit.AddListener(OnJumpKeyChanged);
        slideKeyInputField.onEndEdit.AddListener(OnSlideKeyChanged);
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
    private void OnJumpKeyChanged(string value)
    {
        value = value.ToUpper();
        Debug.Log("JumpKey 저장됨: " + value);
        if (Enum.TryParse(value, out KeyCode result))
        {
            PlayerPrefs.SetString("JumpKey", value);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning($"입력한 키 [{value}]는 유효하지 않습니다.");
            // 예: 메시지로 사용자에게 경고하거나 기본값 설정
        }
    }

    private void OnSlideKeyChanged(string value)
    {
        value = value.ToUpper();

        if (Enum.TryParse(value, out KeyCode parsedKey))
        {
            PlayerPrefs.SetString("SlideKey", value);
            PlayerPrefs.Save();
            Debug.Log($"슬라이드 키 저장됨: {parsedKey}");
        }
        else
        {
            Debug.LogWarning($"입력된 키 [{value}]는 유효하지 않습니다.");
        }
    }
    public void SaveKeyBindings()
    {
        PlayerPrefs.SetString("JumpKey", jumpKeyInputField.text);
        PlayerPrefs.SetString("SlideKey", slideKeyInputField.text);
        PlayerPrefs.Save();
    }
    public void OnSettingChanged()
    {
        SaveKeyBindings();
        PlayerPrefs.SetFloat("BGMVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }
    public void BacktoMain()
    {
        SceneManager.LoadScene("StartUIScene");
    }

}

