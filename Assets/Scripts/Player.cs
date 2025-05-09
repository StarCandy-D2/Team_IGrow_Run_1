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

    [SerializeField] float gravity = -9.8f; //�߷� �������� 
    [SerializeField] float jumpPower = 10f; //������ ��������
    [SerializeField] float BaseRunSpeed;
    [SerializeField] private int maxHp = 5; // ü��
    [SerializeField] float stageInterval = 10f;     //stage time
    [SerializeField] float speedIncreasePerStage = 0.5f; // increase speed per stage ex)3stage =  +1.5f
    [SerializeField] int jumpCount = 0;

    private Vector2 originalSize;
    private Vector2 originalOffset;
    private Vector2 slideSize = new Vector2(1f, 0.5f);
    private Vector2 slideOffset = new Vector2(0f, 0.25f);

    private float verticalSpeed = 0f;
    private float currentRunSpeed;
    public int stage = 0;
    private float elapsedTime = 0f;

    private int currentHp;
    public int CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = Mathf.Clamp(value, 0, maxHp);
        }
    }
    public Slider hpSlider;
    public Slider hpSlider;
    public bool isDead = false; // ���� ���� Ȯ��
    public bool isGod = false;
    bool isFlap = false; // ���� ���� Ȯ��
    private bool isGrounded = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalSize = boxCollider.size;
        originalOffset = boxCollider.offset;
        currentRunSpeed = BaseRunSpeed;

        currentHp = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;
    }

    void Update()
    {
        // �߷� ����
        verticalSpeed += gravity * Time.deltaTime;

        // �� �� �������� Ray �߻�
        Vector2 rayOrigin = transform.position + new Vector3(0, -0.05f, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(rayOrigin, Vector2.down * 0.2f, Color.red);

        bool isGrounded = (hit.collider != null);
        bool isSlideKeyHeld = Input.GetMouseButton(1) || Input.GetKey(KeyCode.E);

        if (!isGrounded && isSlideKeyHeld && verticalSpeed < 0)
        {
            verticalSpeed = -10f;
        }
        // ���� �Է�
        if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")) && jumpCount < 2)
        {
            jumpCount++;
            verticalSpeed = jumpPower;
        }

        // �̵�
        Vector2 move = new Vector2(currentRunSpeed, verticalSpeed);
        transform.Translate(move * Time.deltaTime);

        // �����̵�
        Sliding();

        // �������� ����
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= stageInterval)
        {
            elapsedTime -= stageInterval;
            stage++;
            currentRunSpeed = BaseRunSpeed + stage * speedIncreasePerStage;
        }

        if (hit.collider != null && verticalSpeed <= 0)
        {
            jumpCount = 0;
            verticalSpeed = 0f;

            Vector3 pos = transform.position;
            pos.y = hit.point.y + 0.05f;
            transform.position = pos;
        }
    }

    public void UpdateHPSlider(int amount) // ü�¹� ����
    {
        currentHp -= amount;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        hpSlider.value = currentHp;

        if (currentHp <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    void Sliding()
    {
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.E))
        {
            boxCollider.size = slideSize;
            boxCollider.offset = slideOffset;
        }
        else
        {
            boxCollider.size = originalSize;
            boxCollider.offset = originalOffset;
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
        Debug.Log("Ʈ���� �浹 ����: " + other.gameObject.name);

        if (other.CompareTag("Obstacles")&& !isInvincible)//��ֹ��� �浹 & ������ �ƴϸ� �����
        {
            UpdateHPSlider(1);
            if (isGod) return;


            UpdateHPSlider(1);
            Debug.Log($"��ֹ� Ʈ���� �浹! ���� ü��: {currentHp}");

            if (currentHp <= 0)
            {
                isDead = true;
                GameManager.Instance.GameOver();
            }
            StartCoroutine(InvinciblityCoroutine());
        }
    }
    private IEnumerator InvinciblityCoroutine()
    {
        isInvincible = true;
        Debug.Log("��������");
        yield return new WaitForSeconds(invinciblityDuration);
        isInvincible = false;
        Debug.Log("���� ��");
    }
}
