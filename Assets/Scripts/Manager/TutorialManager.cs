using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] GameObject tutorialUI;
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] MapDataJson mapDataJson;
    GameObject go;

    public bool isFirstRun = true;
    //public bool isTutorialEnd = false;
    bool isTimeSlow = false;

    private void Update()
    {
        if (isFirstRun == false) return;

        if (isTimeSlow == false)
        {
            Debug.DrawRay(Player.position, new Vector2(2, 1), Color.red);
            RaycastHit2D rayHit = Physics2D.Raycast(Player.position, new Vector2(2, 1), 3, 1 << 7);
            if (rayHit.collider == null) return;

            go = rayHit.collider.gameObject;

            if (go.tag == "Obstacles")
            {
                TutorialTime();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                TutorialTimeIsEnd(1);
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                TutorialTimeIsEnd(2);
            }
        }
    }
    void TutorialTime()
    {

    }
    bool TimeSlow()
    {
        if (!isTimeSlow)
        {
            Debug.Log("Slow");
            Time.timeScale = 1f;
            return true;
        }
        return false;
    }



    void TutorialTimeIsEnd(int i)
    {
        if (mapDataJson.mapCode == 0)
        {
            if (i == 1)
            {
                BackToNormal();
            }
        }
        else if (mapDataJson.mapCode == 1)
        {
            if (i == 1)
            {
                BackToNormal();
            }
        }
        else
        {
            if (i == 2)
            {
                BackToNormal();
            }
        }
    }

    void BackToNormal()
    {
        tutorialUI.SetActive(false);
        Time.timeScale = 1f;
        Invoke("IsTimeSlowInvoke", 2f);
    }

    void IsTimeSlowInvoke()
    {
        isTimeSlow = false;
    }
}
