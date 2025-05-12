using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public string CharacterName;

    [Header("�̵�")] //������Ʈ �� ��Űó�� �̵��ӵ� �ٸ� ��Ű ���簡��
    public float baseRunSpeed = 5f;
    public float speedIncreasePerStage = 0.5f;

    [Header("����")]
    public float jumpPower = 10f;
    public int  maxJumpCount = 2; //�⺻ 2ȸ, ȫ�浿 �� ��Ű, ���ڸ� ��Űó�� ���� Ƚ�� �ٸ� ��Ű

    [Header("ü��")]
    public int maxHp = 6;

    
}
