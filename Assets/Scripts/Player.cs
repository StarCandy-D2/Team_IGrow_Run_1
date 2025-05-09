using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    private BoxCollider2D boxCollider;   


    [SerializeField] float gravity = -9.8f; //중력 수동구현 
    [SerializeField] float jumpPower = 10f; //점프력 수동구현
    [SerializeField] float BaseRunSpeed;
    [SerializeField] private int maxHp = 6; // 체력
    [SerializeField] float stageInterval = 10f;     //stage time
    [SerializeField] float speedIncreasePerStage = 0.5f; // increase speed per stage ex)3stage =  +1.5f
    [SerializeField] int jumpCount = 0;


    private Vector2 offsetVec = new Vector2(0, -0.25f); //추후 콜라이더에 따라 수정
    private Vector2 sizeVec = new Vector2(1, 0.5f);     //추후 콜라이더에 따라 수정

    private float verticalSpeed = 0f;
    private float currentRunSpeed;
    public int stage = 0;
    private float elapsedTime = 0f;
    private int currentHp;
    public bool isDead = false; // 죽음 여부 확인
    bool isFlap = false; // 점프 여부 확인
    private bool isGrounded = false;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();    //추후 콜라이더에 따라 수정
        currentRunSpeed = BaseRunSpeed;
        currentHp = maxHp;
    }

    void Update()
    {
        // 중력 적용
        verticalSpeed += gravity * Time.deltaTime;

        // 발 밑 기준으로 Ray 발사
        Vector2 rayOrigin = transform.position + new Vector3(0, -0.5f, 0); // 콜라이더 하단 기준
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(rayOrigin, Vector2.down * 0.2f, Color.red);

        // 점프 입력
        if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")) && jumpCount < 2)
        {
            jumpCount++;
            verticalSpeed = jumpPower;
        }

        // 이동
        Vector2 move = new Vector2(currentRunSpeed, verticalSpeed);
        transform.Translate(move * Time.deltaTime);

        // 슬라이딩
        Sliding();

        // 스테이지 증가
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= stageInterval)
        {
            elapsedTime -= stageInterval;
            stage++;
            currentRunSpeed = BaseRunSpeed + stage * speedIncreasePerStage;
        }

        // 땅 감지 후 보정
        if (hit.collider != null && verticalSpeed <= 0)
        {
            jumpCount = 0;
            verticalSpeed = 0f;

            float colliderHeight = boxCollider.size.y;
            Vector3 pos = transform.position;
            pos.y = hit.point.y + (colliderHeight / 1f);
            transform.position = pos;
        }
    }


    void Sliding()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E))
        {
            Vector2 vec = rigid.velocity;
            vec.y = -jumpPower;
            rigid.velocity = vec;
            boxCollider.offset = offsetVec;     //추후 콜라이더에 따라 수정
            boxCollider.size = sizeVec;         //추후 콜라이더에 따라 수정
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
        Debug.Log("트리거 충돌 감지: " + other.gameObject.name);

        if (other.CompareTag("Obstacles"))
        {
            currentHp--;
            Debug.Log($"장애물 트리거 충돌! 현재 체력: {currentHp}");

            if (currentHp <= 0)
            {
                isDead = true;
                GameManager.Instance.GameOver();
            }
        }
    }


}
