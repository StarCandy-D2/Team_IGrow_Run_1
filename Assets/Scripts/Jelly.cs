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

            // jellyLevel 가져오기 (기본값 1 보장)
            int jellyLevel = ShopPlayer.Instance != null ? ShopPlayer.Instance.jellyLevel : 1;

            // 점수 계산: baseScore × 레벨
            int finalScore = baseScore + (jellyLevel - 1) * scoreStep;

            Debug.Log($"젤리 먹기! jellyLevel: {jellyLevel}, 점수 획득: {finalScore}");
            GameManager.Instance.AddScore(finalScore);

            // 애니메이션 재생
            if (animator != null)
            {
                animator.SetBool("IsEat", true);
            }

            // 일정 시간 후 오브젝트 제거
            Destroy(this.gameObject, 0.2f);
        }
    }
}
