using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsKeyBinder : MonoBehaviour
{
    public TMP_Text jumpKeyText;
    public Button jumpKeyButton;

    public TMP_Text slideKeyText;
    public Button slideKeyButton;

    private bool isBindingJump = false;
    private bool isBindingSlide = false;

    private void Start()
    {
        // 초기 키 로드
        string savedJumpKey = PlayerPrefs.GetString("JumpKey", "Space");
        string savedSlideKey = PlayerPrefs.GetString("SlideKey", "LeftShift");

        jumpKeyText.text = $"Jump: {savedJumpKey}";
        slideKeyText.text = $"Slide: {savedSlideKey}";

        // 버튼에 리스너 추가
        jumpKeyButton.onClick.AddListener(() =>
        {
            isBindingJump = true;
            jumpKeyText.text = "Press any key...";
        });

        slideKeyButton.onClick.AddListener(() =>
        {
            isBindingSlide = true;
            slideKeyText.text = "Press any key...";
        });
    }

    private void Update()
    {
        if (!isBindingJump && !isBindingSlide) return;

        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                if (isBindingJump)
                {
                    PlayerPrefs.SetString("JumpKey", key.ToString());
                    PlayerPrefs.Save();
                    jumpKeyText.text = $"Jump: {key}";
                    isBindingJump = false;
                }
                else if (isBindingSlide)
                {
                    PlayerPrefs.SetString("SlideKey", key.ToString());
                    PlayerPrefs.Save();
                    slideKeyText.text = $"Slide: {key}";
                    isBindingSlide = false;
                }

                break;
            }
        }
    }
}
