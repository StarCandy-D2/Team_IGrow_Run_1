using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public string CharacterName;

    [Header("이동")] //스케이트 맛 쿠키처럼 이동속도 다른 쿠키 존재가능
    public float baseRunSpeed = 5f;
    public float speedIncreasePerStage = 0.5f;

    [Header("점프")]
    public float jumpPower = 10f;
    public int  maxJumpCount = 2; //기본 2회, 홍길동 맛 쿠키, 닌자맛 쿠키처럼 점프 횟수 다른 쿠키

    [Header("체력")]
    public int maxHp = 6;

    
}
