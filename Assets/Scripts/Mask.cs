using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] PrefebManager pref;
    [SerializeField] MapDataJson mapDataScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Block")
        {
            mapDataScript.GetCode();
            pref.SetBlock();
        }
    }
}
