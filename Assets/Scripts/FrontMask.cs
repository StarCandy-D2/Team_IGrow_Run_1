using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontMask : MonoBehaviour
{
    int count = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Block")
        {
            count++;
            if (count == 2)
            {
                GameManager.Instance.stage++;
                count = 0;
            }
        }
    }
}
