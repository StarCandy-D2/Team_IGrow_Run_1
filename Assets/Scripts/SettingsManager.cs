using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{

    public Slider volumeSlider;
    public TMP_InputField volumeInputField;

    public TMP_InputField jumpKeyInputField;
    public TMP_InputField slideKeyInputField;

    private AudioSource bgmSource;

    void Start()
    {
        // �����̴� ���� ���� ����
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 100f;
        volumeSlider.wholeNumbers = true;

        // �ʱ�ȭ
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 100f);

        //0~1 ������ �߸� ����� ���̸� �� 0~100 �������� ��ȯ
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
    }

    public void OnSliderChanged(float value)
    {
        bgmSource.volume = value / 100f;
        volumeInputField.text = value.ToString("0") + "%";
        PlayerPrefs.SetFloat("BGMVolume", value);
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

