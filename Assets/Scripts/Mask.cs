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
            mapDataScript.GetCode();     // �� ������ ����
            pref.SetBlock();

            //StartCoroutine(DelayBlockSpawn());
        }
    }

    //IEnumerator DelayBlockSpawn()
    //{
    //    yield return new WaitForSeconds(3f); //  3�� ���

    //    mapDataScript.GetCode();     // �� ������ ����
    //    pref.SetBlock();             // ��� ����

    //    hasTriggered = false;        // �ٽ� �浹 ���
    //}
}
