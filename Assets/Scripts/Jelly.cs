using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public Animator animator;
    int score = 0;
    bool hasEaten = false;
    [SerializeField] int scoreValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasEaten && collision.CompareTag("Player")) // Ʈ���Ű� �ι� �۵��ϴ� ���� �����ϱ����� ���ǹ�
        {
            hasEaten = true;
            Debug.Log(++score); // UI�� ���ھ�� �����Ͽ� ���
            Debug.Log("���� �Ա�");
            GameManager.Instance.AddScore(scoreValue);
            animator.SetBool("IsEat", true);

            Destroy(this.gameObject, 0.2f);
        }

    }
}
