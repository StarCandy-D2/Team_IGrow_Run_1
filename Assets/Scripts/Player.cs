using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    private BoxCollider2D boxCollider;   


    [SerializeField] float gravity = -9.8f; //�߷� �������� 
    [SerializeField] float jumpPower = 10f; //������ ��������
    [SerializeField] float BaseRunSpeed;
    [SerializeField] private int maxHp = 6; // ü��
    [SerializeField] float stageInterval = 10f;     //stage time
    [SerializeField] float speedIncreasePerStage = 0.5f; // increase speed per stage ex)3stage =  +1.5f
    [SerializeField] int jumpCount = 0;


    private Vector2 offsetVec = new Vector2(0, -0.25f); //���� �ݶ��̴��� ���� ����
    private Vector2 sizeVec = new Vector2(1, 0.5f);     //���� �ݶ��̴��� ���� ����

    private float verticalSpeed = 0f;
    private float currentRunSpeed;
    public int stage = 0;
    private float elapsedTime = 0f;
    private int currentHp;
    public bool isDead = false; // ���� ���� Ȯ��
    bool isFlap = false; // ���� ���� Ȯ��
    private bool isGrounded = false;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();    //���� �ݶ��̴��� ���� ����
        currentRunSpeed = BaseRunSpeed;
        currentHp = maxHp;
    }

    void Update()
    {
        // �߷� ����
        verticalSpeed += gravity * Time.deltaTime;

        // �� �� �������� Ray �߻�
        Vector2 rayOrigin = transform.position + new Vector3(0, -0.5f, 0); // �ݶ��̴� �ϴ� ����
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(rayOrigin, Vector2.down * 0.2f, Color.red);

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

        // �� ���� �� ����
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
            boxCollider.offset = offsetVec;     //���� �ݶ��̴��� ���� ����
            boxCollider.size = sizeVec;         //���� �ݶ��̴��� ���� ����
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

        if (other.CompareTag("Obstacles"))
        {
            currentHp--;
            Debug.Log($"��ֹ� Ʈ���� �浹! ���� ü��: {currentHp}");

            if (currentHp <= 0)
            {
                isDead = true;
                GameManager.Instance.GameOver();
            }
        }
    }


}
