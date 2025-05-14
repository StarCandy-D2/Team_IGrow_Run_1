using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static MapDataJson;

public class PrefebManager : MonoBehaviour
{
    [SerializeField] GameObject GroundPrefeb;
    [SerializeField] GameObject PlatformPrefeb;
    [SerializeField] GameObject JumpObstaclePrefeb;
    [SerializeField] GameObject DoubleJumpObstaclePrefeb;
    [SerializeField] GameObject SlideObstaclePrefeb;
    [SerializeField] GameObject JellyPrefeb;

    [SerializeField] Transform Block1;
    [SerializeField] Transform Block2;

    List<GameObject> Block1GroundList = new List<GameObject>();
    List<GameObject> Block1PlatformList = new List<GameObject>();
    List<GameObject> Block1JumpObstaclePrefebList = new List<GameObject>();
    List<GameObject> Block1DoubleJumpObstaclePrefebList = new List<GameObject>();
    List<GameObject> Block1SlideObstaclePrefebList = new List<GameObject>();
    List<GameObject> Block1JellyPrefebList = new List<GameObject>();

    List<GameObject> Block2GroundList = new List<GameObject>();
    List<GameObject> Block2PlatformList = new List<GameObject>();
    List<GameObject> Block2JumpObstaclePrefebList = new List<GameObject>();
    List<GameObject> Block2DoubleJumpObstaclePrefebList = new List<GameObject>();
    List<GameObject> Block2SlideObstaclePrefebList = new List<GameObject>();
    List<GameObject> Block2JellyPrefebList = new List<GameObject>();

    Vector2 lastBlockPosition = new Vector2(-20, -4.5f);
    [SerializeField] MapDataJson mapDataScript;
    Vector2 remote = new Vector2(0, -20);
    Quaternion quaternion = Quaternion.identity;
    int num = 0;
    private bool isFirstBlock = true;
    readonly float firstX = 15f;
    readonly float nextX = 25f;
    readonly float fixedY = -4.5f;
    private void Start()
    {
        InstantiatePrefebs();
        SetBlock();
    }

    void InstantiatePrefebs()
    {
        for (int i = 0; i < 16; i++)
        {
            Block1GroundList.Add(Instantiate(GroundPrefeb, remote, quaternion, Block1.GetChild(0)));
            Block2GroundList.Add(Instantiate(GroundPrefeb, remote, quaternion, Block2.GetChild(0)));
        }
        for (int i = 0; i < 6; i++)
        {
            Block1PlatformList.Add(Instantiate(PlatformPrefeb, remote, quaternion, Block1.GetChild(1)));
            Block2PlatformList.Add(Instantiate(PlatformPrefeb, remote, quaternion, Block2.GetChild(1)));
        }
        for (int i = 0; i < 4; i++)
        {
            Block1JumpObstaclePrefebList.Add(Instantiate(JumpObstaclePrefeb, remote, quaternion, Block1.GetChild(1)));
            Block2JumpObstaclePrefebList.Add(Instantiate(JumpObstaclePrefeb, remote, quaternion, Block2.GetChild(1)));
        }
        for (int i = 0; i < 4; i++)
        {
            Block1DoubleJumpObstaclePrefebList.Add(Instantiate(DoubleJumpObstaclePrefeb, remote, quaternion, Block1.GetChild(1)));
            Block2DoubleJumpObstaclePrefebList.Add(Instantiate(DoubleJumpObstaclePrefeb, remote, quaternion, Block2.GetChild(1)));
        }
        for (int i = 0; i < 4; i++)
        {
            Block1SlideObstaclePrefebList.Add(Instantiate(SlideObstaclePrefeb, remote, quaternion, Block1.GetChild(1)));
            Block2SlideObstaclePrefebList.Add(Instantiate(SlideObstaclePrefeb, remote, quaternion, Block2.GetChild(1)));
        }
        for (int i = 0; i < 40; i++)
        {
            Block1JellyPrefebList.Add(Instantiate(JellyPrefeb, remote, quaternion, Block1.GetChild(2)));
            Block2JellyPrefebList.Add(Instantiate(JellyPrefeb, remote, quaternion, Block2.GetChild(2)));
        }
        Block1.gameObject.SetActive(false);
        Block2.gameObject.SetActive(false);
    }
    private void Update()
    {

        int blockTurn = num % 2;
        Transform block = blockTurn == 1 ? Block1 : Block2;


    }
    public void SetBlock()
    {
        int blockTurn = num % 2;

        float xPos = isFirstBlock ? firstX : nextX;
        lastBlockPosition = new Vector2(xPos, fixedY);

        Set(blockTurn);
        num++;
        isFirstBlock = false;
    }

    void Set(int blockTurn)
    {

        List<GameObject> ground = blockTurn == 1 ? Block1GroundList : Block2GroundList;
        List<GameObject> Platform = blockTurn == 1 ? Block1PlatformList : Block2PlatformList;
        List<GameObject> Obstacle1 = blockTurn == 1 ? Block1JumpObstaclePrefebList : Block2JumpObstaclePrefebList;
        List<GameObject> Obstacle2 = blockTurn == 1 ? Block1DoubleJumpObstaclePrefebList : Block2DoubleJumpObstaclePrefebList;
        List<GameObject> Obstacle3 = blockTurn == 1 ? Block1SlideObstaclePrefebList : Block2SlideObstaclePrefebList;
        List<GameObject> jelly = blockTurn == 1 ? Block1JellyPrefebList : Block2JellyPrefebList;


        Vector2[] GroundVec = mapDataScript.ReturnGroundCode();
        Vector2[] PlatformVec = mapDataScript.ReturnPlatformCode();
        Vector2[] JumpObstacleVec = mapDataScript.ReturnObstacle1Code();
        Vector2[] DoubleJumpObstacleVec = mapDataScript.ReturnObstacle2Code();
        Vector2[] SlideObstacleVec = mapDataScript.ReturnObstacle3Code();
        int[][] jellySets = mapDataScript.ReturnJellySets() == null ? null : mapDataScript.ReturnJellySets();

        JellyStruct jellyStruct = mapDataScript.jellyStruct;

        Transform block = blockTurn == 1 ? Block1 : Block2;

        block.position = lastBlockPosition;
        block.gameObject.SetActive(true);

        SetPrefebs(GroundVec, ground);
        SetPrefebs(PlatformVec, Platform);
        SetPrefebs(JumpObstacleVec, Obstacle1);
        SetPrefebs(DoubleJumpObstacleVec, Obstacle2);
        SetPrefebs(SlideObstacleVec, Obstacle3);

        int[] setJelly1 = SetJelly(JumpObstacleVec, jellyStruct.JumpObsJellyVec, jelly, new int[] {-1, 0}, jellySets);
        int[] setJelly2 = SetJelly(DoubleJumpObstacleVec, jellyStruct.DblObsJellyVec, jelly, setJelly1, jellySets);
        int[] setJelly3 = SetJelly(SlideObstacleVec, jellyStruct.SlideObsJellyVec, jelly, setJelly2, jellySets);

        ResetJelly(jelly, setJelly3[0]);

        block.position = lastBlockPosition;
        block.gameObject.SetActive(true);
    }

    void SetPrefebs(Vector2[] prefebVec, List<GameObject> prefeb)
    {
        if (prefebVec != null)
        {
            for (int i = 0; i < prefebVec.Length; i++)
            {
                prefeb[i].transform.localPosition = prefebVec[i];
            }
            for (int j = prefebVec.Length; j < prefeb.Count; j++)
            {
                prefeb[j].transform.localPosition = remote;
            }
        }
        else if (prefebVec == null)
        {
            for (int i = 0; i < prefeb.Count; i++)
            {
                prefeb[i].transform.localPosition = remote;
            }
        }
    }

    int[] SetJelly(Vector2[] ObstacleVec, Vector2[] JellyVec, List<GameObject> jelly, int[] iValue, int[][] jellySets)

    {
        if (ObstacleVec != null)
        {
            Vector2[] vec = new Vector2[JellyVec.Length];
            int[] iNum = new int[2];
            if (iValue[0] < 0)
            {
                iValue[0]++;
                iNum[0] = iValue[0];
                iNum[1] = iValue[1];
            }
            else
            {
                iNum[0] = iValue[0];
                iNum[1] = iValue[1];
            }
            for (int i = 0; i < ObstacleVec.Length; i++)
            {
                int plus = jellySets == null ? 0 : jellySets[iNum[1]][0];
                int minus = jellySets == null ? 0 : jellySets[iNum[1]][1];
 
                int jellyCount = JellyVec.Length - minus - plus;
                for (int j = 0; j < jellyCount; j++)
                {
                    iNum[0] = iNum[0] + 1;
                    iNum[0] = i > 0 && j == 0 ? iNum[0]++ : iNum[0];
                    vec[j].x = JellyVec[j + plus].x + ObstacleVec[i].x;
                    vec[j].y = JellyVec[j + plus].y;

                    jelly[iNum[0] -1].transform.localPosition = vec[j];
                }
                iNum[1]++;
            }            
            return iNum;
        }
        else
        {
            int[] iNum = new int[2];
            if (iValue[0] < 0)
            {
                iValue[0]++;
                iNum[0] = iValue[0];
                iNum[1] = iValue[1];
                return iNum;
            }
            else
            {
                iNum[0] = iValue[0];
                iNum[1] = iValue[1];
                return iNum;
            }
                //Debug.Log("Inum = " + iValue);
        }
    }

    void ResetJelly(List<GameObject> jelly, int iValue)
    {
        for (int i = iValue; i < jelly.Count; i++)

        {
            jelly[i].transform.localPosition = remote;
            jelly[i].SetActive(true);
        }
    }
}
