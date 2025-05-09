using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapDataJson;

public class PrefebManager : MonoBehaviour
{
    [SerializeField] GameObject GroundPrefeb;
    [SerializeField] GameObject JumpObstaclePrefeb;
    [SerializeField] GameObject DoubleJumpObstaclePrefeb;
    [SerializeField] GameObject SlideObstaclePrefeb;

    [SerializeField] Transform Block1;
    [SerializeField] Transform Block2;

    List<GameObject> Block1GroundList = new List<GameObject>();
    List<GameObject> Block1JumpObstaclePrefebList = new List<GameObject>();
    List<GameObject> Block1DoubleJumpObstaclePrefebList = new List<GameObject>();
    List<GameObject> Block1SlideObstaclePrefebList = new List<GameObject>();

    List<GameObject> Block2GroundList = new List<GameObject>();    
    List<GameObject> Block2JumpObstaclePrefebList = new List<GameObject>();    
    List<GameObject> Block2DoubleJumpObstaclePrefebList = new List<GameObject>();    
    List<GameObject> Block2SlideObstaclePrefebList = new List<GameObject>();

    Vector2 lastBlockPosition = new Vector2(-20, -4.5f);
    [SerializeField] MapDataJson mapDataScript;
    Vector2 remote = new Vector2(0, -20);
    Quaternion quaternion = Quaternion.identity;
    int num = 0;

    private void Start()
    {
        InstantiatePrefebs();
        SetBlock();
    }

    void InstantiatePrefebs()
    {
        for (int i = 0; i < 16; i++)
        {
            Block1GroundList.Add(Instantiate(GroundPrefeb, remote, quaternion, Block1));
            Block2GroundList.Add(Instantiate(GroundPrefeb, remote, quaternion, Block2));
        }
        for (int i = 0; i < 8; i++)
        {
            Block1JumpObstaclePrefebList.Add(Instantiate(JumpObstaclePrefeb, remote, quaternion, Block1));
            Block2JumpObstaclePrefebList.Add(Instantiate(JumpObstaclePrefeb, remote, quaternion, Block2));
        }
        for (int i = 0; i < 5; i++)
        {
            Block1DoubleJumpObstaclePrefebList.Add(Instantiate(DoubleJumpObstaclePrefeb, remote, quaternion, Block1));
            Block2DoubleJumpObstaclePrefebList.Add(Instantiate(DoubleJumpObstaclePrefeb, remote, quaternion, Block2));
        }
        for (int i = 0; i < 8; i++)
        {
            Block1SlideObstaclePrefebList.Add(Instantiate(SlideObstaclePrefeb, remote, quaternion, Block1));
            Block2SlideObstaclePrefebList.Add(Instantiate(SlideObstaclePrefeb, remote, quaternion, Block2));
        }

        Block1.gameObject.SetActive(false);
        Block2.gameObject.SetActive(false);
    }

    public void SetBlock()
    {
        num++;
        int blockTrun = num % 2;
        lastBlockPosition.x += 40;

        Set(blockTrun);
    }

    void Set(int blockTrun)
    {
        List<GameObject> ground = blockTrun == 1 ? Block1GroundList : Block2GroundList;
        List<GameObject> Obstacle1 = blockTrun == 1 ? Block1JumpObstaclePrefebList : Block2JumpObstaclePrefebList;
        List<GameObject> Obstacle2 = blockTrun == 1 ? Block1DoubleJumpObstaclePrefebList : Block2DoubleJumpObstaclePrefebList;
        List<GameObject> Obstacle3 = blockTrun == 1 ? Block1SlideObstaclePrefebList : Block2SlideObstaclePrefebList;

        Vector2[] GroundVec = mapDataScript.ReturnGroundCode();
        Vector2[] JumpObstacleVec = mapDataScript.ReturnObstacle1Code();
        Vector2[] DoubleJumpObstacleVec = mapDataScript.ReturnObstacle2Code();
        Vector2[] SlideObstacleVec = mapDataScript.ReturnObstacle3Code();

        Transform block = blockTrun == 1 ? Block1 : Block2;

        if (GroundVec != null)
        {
            for (int i = 0; i < GroundVec.Length; i++)
            {
                ground[i].transform.localPosition = GroundVec[i];
            }
        }
        if (JumpObstacleVec != null)
        {
            for (int i = 0; i < JumpObstacleVec.Length; i++)
            {
                Obstacle1[i].transform.localPosition = JumpObstacleVec[i];
            }
        }
        if (DoubleJumpObstacleVec != null)
        {
            for (int i = 0; i < DoubleJumpObstacleVec.Length; i++)
            {
                Obstacle2[i].transform.localPosition = DoubleJumpObstacleVec[i];
            }
        }
        if (SlideObstacleVec != null)
        {
            for (int i = 0; i < SlideObstacleVec.Length; i++)
            {
                Obstacle3[i].transform.localPosition = SlideObstacleVec[i];
            }
        }

        block.position = lastBlockPosition;
        block.gameObject.SetActive(true);
    }
}
