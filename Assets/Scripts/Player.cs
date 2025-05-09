using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    private BoxCollider2D boxCollider;                  //���� �ݶ��̴��� ���� ����
    private Vector2 offsetVec = new Vector2(0, -0.25f); //���� �ݶ��̴��� ���� ����
    private Vector2 sizeVec = new Vector2(1, 0.5f);     //���� �ݶ��̴��� ���� ����
    [SerializeField] float jumpPower;
    [SerializeField] float BaseRunSpeed;
    
    private float currentRunSpeed;
    
    [SerializeField] int jumpCount = 0;

    public int stage = 0;
    [SerializeField] float stageInterval = 10f;     //stage time
    [SerializeField] float speedIncreasePerStage = 0.5f; // increase speed per stage ex)3stage =  +1.5f
    private float elapsedTime = 0f;

    public bool isDead = false; // ���� ���� Ȯ��
    public bool isGod = false;
    bool isFlap = false; // ���� ���� Ȯ��

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();    //���� �ݶ��̴��� ���� ����
        currentRunSpeed = BaseRunSpeed;
    }

    private void Update()
    {
        RunAndJump();
        Sliding();
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= stageInterval)
        {
            elapsedTime -= stageInterval;
            stage++;
            currentRunSpeed = BaseRunSpeed + stage * speedIncreasePerStage;  // ex 3stage  -> 3(base) + 3*0.5 = 4.5f
        }
    }

        void RunAndJump()
    {
        Vector2 vec = rigid.velocity;
        vec.x = currentRunSpeed;

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            if(jumpCount < 2)
            {
                jumpCount++;
                vec.y = jumpPower;
            }
        }

        rigid.velocity = vec;
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
        if(collision.transform.tag == "Ground")
        {
            jumpCount = 0;
            return;
        }


        if (collision.transform.tag == "Obstacles")
        {
            if (isGod) return;

            isDead = true;

            GameManager.Instance.GameOver();
        }

    }
}
