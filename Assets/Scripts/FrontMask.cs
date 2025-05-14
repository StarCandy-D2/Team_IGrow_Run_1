using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontMask : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] MapDataJson mapData;
    int count = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            mapData.AdvanceStage(); // stageCode ¼øÈ¯
            if (count == 3)
            {
                tutorialManager.isFirstRun = false;
                count = 0;
            }
        }
    }
}
