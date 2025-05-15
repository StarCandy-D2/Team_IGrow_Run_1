using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public Animator animator;
    //bool hasEaten = false;

    [SerializeField] int baseScore = 1;
    [SerializeField] int scoreStep = 2;

    [SerializeField] private JellyLevelData jellyLevelData;

    [SerializeField] private AudioClip eatSFX;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (ShopPlayer.Instance != null && jellyLevelData != null)
        {
            int lvl = Mathf.Clamp(ShopPlayer.Instance.jellyLevel, 1, jellyLevelData.levelSprites.Count);
            spriteRenderer.sprite = jellyLevelData.levelSprites[lvl-1];
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //!hasEaten && 
        {
            //hasEaten = true;

            int jellyLevel = ShopPlayer.Instance != null ? ShopPlayer.Instance.jellyLevel : 1;
            int finalScore = baseScore + (jellyLevel - 1) * scoreStep;

            GameManager.Instance.AddScore(finalScore);

            if (audioSource != null && eatSFX != null)
            {
                audioSource.PlayOneShot(eatSFX); // È¿°úÀ½
            }

            if (animator != null)
            {
                animator.SetBool("IsEat", true);
            }
            StartCoroutine(DisableAfterDelay(0.2f));
        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
