using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Transform Player;
    GameObject go;
    public bool isFirstRun = true;
    public bool isTutorialEnd = false;
    bool isTimeSlow = false;

    private void Update()
    {
        if (isTutorialEnd == true) return;

        Vector2 vec = Player.position;
        vec.y -= 0.5f;
        Debug.DrawRay(Player.position, new Vector2(2, 1), Color.red);
        RaycastHit2D rayHit = Physics2D.Raycast(Player.position, new Vector2(2, 1), 3, 1 << 7);
        if (rayHit.collider == null) return;

        go = rayHit.collider.gameObject;

        if (go.tag == "Obstacles")
        {
            //시간 느려지게
            TimeSlow();
            //Ui띄우기           
        }
    }
    //private void FixedUpdate()
    //{        
        

    //}

    void TimeSlow()
    {
        if (!isTimeSlow)
        {
            Debug.Log("Slow");
            Time.timeScale = 0.1f;
            isTimeSlow = true;            
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Fast");
                Time.timeScale = 1;
                Invoke("TimeSlowFalse", 2);
            }
        }
    }

    void TimeSlowFalse()
    {
        isTimeSlow = false;
    }
}
