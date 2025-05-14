using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    private BoxCollider2D boxCollider;


    [SerializeField] private float invinciblityDuration = 1.0f;
    private bool isInvincible = false;

    [SerializeField] float gravity = -9.8f; //중력 수동구현 
    [SerializeField] float jumpPower = 10f; //점프력 수동구현
    [SerializeField] float BaseRunSpeed;
    [SerializeField] private int maxHp = 5; // 체력
    
    [SerializeField] float speedIncreasePerStage = 0.5f; // increase speed per stage ex)3stage =  +1.5f
    [SerializeField] int jumpCount = 0;
    [SerializeField] private AnimationHandler animationHandler;
    public int JumpCount => jumpCount;

    private Vector2 originalSize;
    private Vector2 originalOffset;
    private Vector2 slideSize = new Vector2(1f, 0.5f);
    private Vector2 slideOffset = new Vector2(0f, -0.25f);

    private float verticalSpeed = 0f;
    private float currentRunSpeed;
    public int stage = 0;

    private KeyCode jumpKey;
    private KeyCode slideKey;

    private float timeElapsed = 0f;


    private int currentHp;
    public int jellylevel = 1;
    public int maxHPlevel = 1;
    public int otherlevel = 1;
    public int lifelevel = 1;
    public int CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = Mathf.Clamp(value, 0, maxHp);
        }
    }

    public Slider hpSlider;



    public int coins = 0; // 플레이어 가지고 있는 코인 갯수
    

    public bool isDead = false; // 죽음 여부 확인
    public bool isGod = false; // 무적 여부 확인
    bool isFlap = false; // 점프 여부 확인
    private bool isGrounded = false;

    private void Start()
    {
        //디버깅용 코드
        string jumpKeyStr = PlayerPrefs.GetString("JumpKey", "Space");
        string slideKeyStr = PlayerPrefs.GetString("SlideKey", "LeftShift");
        //
        jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpKey", "Space"));
        slideKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SlideKey", "LeftShift"));

        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalSize = boxCollider.size;
        originalOffset = boxCollider.offset;
        currentRunSpeed = BaseRunSpeed;

        animationHandler = GetComponent<AnimationHandler>();
        
        if (ShopPlayer.Instance != null)
        {
            jellylevel = ShopPlayer.Instance.jellyLevel;
            maxHPlevel = ShopPlayer.Instance.maxHPLevel;
            coins = ShopPlayer.Instance.coins;

            maxHp = ShopPlayer.Instance.GetMaxHP();
            currentHp = maxHp;
            {
                Debug.Log($"ShopPlayer maxHPLevel: {ShopPlayer.Instance.maxHPLevel}");
                maxHp = ShopPlayer.Instance.GetMaxHP();
            }
        }
        if (hpSlider == null)
        {
            Debug.LogError(" hpSlider가 null입니다! Inspector에 연결되어 있는지 확인하세요.");
        }

        hpSlider.maxValue = maxHp;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp); // 혹시 모를 대비
        hpSlider.value = currentHp;
        Debug.Log($"currentHp 초기화 후 값: {currentHp}");
        //디버깅용 코드
        if (!Enum.TryParse(jumpKeyStr, out jumpKey))
        {
            Debug.LogWarning($"잘못된 점프 키 [{jumpKeyStr}], 기본값 'Space'로 설정");
            jumpKey = KeyCode.Space;
        }

        if (!Enum.TryParse(slideKeyStr, out slideKey))
        {
            Debug.LogWarning($"잘못된 슬라이드 키 [{slideKeyStr}], 기본값 'LeftShift'로 설정");
            slideKey = KeyCode.LeftShift;
        }

    }

    // 발 밑 기준으로 Ray 발사
    public bool IsGrounded()
    {
        Vector2 rayOrigin = transform.position + new Vector3(0, 0.01f, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 1.3f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    void Update()
    {
        // 중력 적용
        verticalSpeed += gravity * Time.deltaTime;

        bool isGrounded = IsGrounded();

        Vector2 rayOrigin = transform.position + new Vector3(0, 0.01f, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 1.3f, LayerMask.GetMask("Ground"));

        if (isGrounded && verticalSpeed <= 0)
        {
            jumpCount = 0;
            verticalSpeed = 0f;

            Vector3 pos = transform.position;
            pos.y = hit.point.y + 1.2f;
            transform.position = pos;
        }

        // 점프 입력
        if (Input.GetKeyDown(jumpKey) && jumpCount < 2)
        {
            jumpCount++;
            verticalSpeed = jumpPower;
        }

        // 슬라이딩
        if (Input.GetKey(slideKey))
        {
            boxCollider.size = slideSize;
            boxCollider.offset = slideOffset;

            // 공중에서 슬라이딩 시 급하강 처리
            if (!isGrounded && verticalSpeed < 0)
            {
                verticalSpeed = -10f;
            }
        }
        else
        {
            boxCollider.size = originalSize;
            boxCollider.offset = originalOffset;
        }

        // 이동
        Vector2 move = new Vector2(currentRunSpeed, verticalSpeed);
        transform.Translate(move * Time.deltaTime);

        // 스테이지 증가
        float interval = GameManager.Instance.stageInterval;
        int currentStage = GameManager.Instance.stage;
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= interval)
        {
            timeElapsed = 0;
        }
        currentRunSpeed = BaseRunSpeed + (currentStage - 1) * speedIncreasePerStage;

        if (hit.collider != null && verticalSpeed <= 0)
        {
            jumpCount = 0;
            verticalSpeed = 0f;

            Vector3 pos = transform.position;
            pos.y = hit.point.y + 1.2f;
            transform.position = pos;
        }
    }

    public void UpdateHPSlider(int amount)
    {
        currentHp -= amount;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        hpSlider.value = currentHp;


        if (currentHp <= 0)
        {
            isDead = true;
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
            return;
        if (collision.transform.CompareTag("Ground"))
        {
            jumpCount = 0;
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Obstacles") && !isInvincible)
        {
            if (isGod) return;

            UpdateHPSlider(1);          // 체력 깎기
            GetComponent<AnimationHandler>().FlashDamageColor(); // 데미지 입을시 색 변경
            StartCoroutine(InvinciblityCoroutine());  // 무적 시작
        }
    }
    private IEnumerator InvinciblityCoroutine()
    {
        isInvincible = true;
        Debug.Log("무적시작");
        yield return new WaitForSeconds(invinciblityDuration);
        isInvincible = false;
        Debug.Log("무적 끝");
    }
}
