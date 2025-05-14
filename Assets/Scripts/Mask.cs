using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] PrefebManager pref;
    [SerializeField] MapDataJson mapDataScript;
    //private bool hasTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block")) //&&!hasTriggered
        {
            //hasTriggered = true;
            mapDataScript.GetCode();     // 맵 데이터 갱신
            pref.SetBlock();

            //StartCoroutine(DelayBlockSpawn());
        }
    }

    //IEnumerator DelayBlockSpawn()
    //{
    //    yield return new WaitForSeconds(3f); //  3초 대기

    //    mapDataScript.GetCode();     // 맵 데이터 갱신
    //    pref.SetBlock();             // 블록 생성

    //    hasTriggered = false;        // 다시 충돌 허용
    //}
}
