using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public Animator animator;
    bool hasEaten = false;

    [SerializeField] int baseScore = 1;
    [SerializeField] int scoreStep = 2;

    [SerializeField] private JellyLevelData jellyLevelData;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (ShopPlayer.Instance != null && jellyLevelData != null)
        {
            int lvl = Mathf.Clamp(ShopPlayer.Instance.jellyLevel, 1, jellyLevelData.levelSprites.Count);
            spriteRenderer.sprite = jellyLevelData.levelSprites[lvl-1];
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasEaten && collision.CompareTag("Player"))
        {
            hasEaten = true;

            // jellyLevel �������� (�⺻�� 1 ����)
            int jellyLevel = ShopPlayer.Instance != null ? ShopPlayer.Instance.jellyLevel : 1;

            // ���� ���: baseScore �� ����
            int finalScore = baseScore + (jellyLevel - 1) * scoreStep;

            Debug.Log($"���� �Ա�! jellyLevel: {jellyLevel}, ���� ȹ��: {finalScore}");
            GameManager.Instance.AddScore(finalScore);

            // �ִϸ��̼� ���
            if (animator != null)
            {
                animator.SetBool("IsEat", true);
            }

            // ���� �ð� �� ������Ʈ ����
            Destroy(this.gameObject, 0.2f);
        }
    }
}
