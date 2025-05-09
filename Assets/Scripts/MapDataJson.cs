using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class MapDataJson : MonoBehaviour
{
    public struct PrefebsStruct
    {
        public float[] Ground { get; private set; }
        public float[] JumpObstacle { get; private set; }
        public float[] DoubleJumpObstacle { get; private set; }
        public float[] SlideObstacle { get; private set; }
        public PrefebsStruct(float[] ground, float[] jumpObstacle, float[] doubleJumpObstacle, float[] slideObstacle)
        {
            Ground = ground; JumpObstacle = jumpObstacle; DoubleJumpObstacle = doubleJumpObstacle; SlideObstacle = slideObstacle;
        }
    }

    PrefebsStruct[] prefebsStruct;

    Vector2[] GroundVec;
    Vector2[] jumpObstacleVec;
    Vector2[] doubleJumpObstacleVec;
    Vector2[] slideObstacleVec;

    int mapCode;

    private void Start()
    {
        string jsonString = Resources.Load("MapData").ToString();
        prefebsStruct = JsonConvert.DeserializeObject<PrefebsStruct[]>(jsonString);

        GetCode();
    }
    public void GetCode()
    {
        mapCode = GetRandomCode();

        GetGroundCode();
        GetJumpObstacleCode();
        GetDoubleJumpObstaclCode();
        GetSlideObstacleCode();
    }
    void GetGroundCode()
    {
        if (prefebsStruct[mapCode].Ground != null)
        {
            GroundVec = new Vector2[prefebsStruct[mapCode].Ground.Length];
            for (int i = 0; i < prefebsStruct[mapCode].Ground.Length; i++)
            {
                GroundVec[i] = new Vector2(prefebsStruct[mapCode].Ground[i], 0f);
            }
        }
    }

    void GetJumpObstacleCode()
    {
        if (prefebsStruct[mapCode].JumpObstacle != null)
        {
            jumpObstacleVec = new Vector2[prefebsStruct[mapCode].JumpObstacle.Length];
            for (int i = 0; i < prefebsStruct[mapCode].JumpObstacle.Length; i++)
            {
                jumpObstacleVec[i] = new Vector2(prefebsStruct[mapCode].JumpObstacle[i], 1f);
            }
        }
    }

    void GetDoubleJumpObstaclCode()
    {       
        if (prefebsStruct[mapCode].DoubleJumpObstacle != null)
        {
            doubleJumpObstacleVec = new Vector2[prefebsStruct[mapCode].DoubleJumpObstacle.Length];
            for (int i = 0; i < prefebsStruct[mapCode].DoubleJumpObstacle.Length; i++)
            {
                doubleJumpObstacleVec[i] = new Vector2(prefebsStruct[mapCode].DoubleJumpObstacle[i], 1.5f);
            }
        }
    }

    void GetSlideObstacleCode()
    {
        if (prefebsStruct[mapCode].SlideObstacle != null)
        {
            slideObstacleVec = new Vector2[prefebsStruct[mapCode].SlideObstacle.Length];
            for (int i = 0; i < prefebsStruct[mapCode].SlideObstacle.Length; i++)
            {
                slideObstacleVec[i] = new Vector2(prefebsStruct[mapCode].SlideObstacle[i], 4.9f);
            }
        }
    }

    public Vector2[] ReturnGroundCode()
    {
        return GroundVec;
    }
    public Vector2[] ReturnObstacle1Code()
    {
        return jumpObstacleVec;
    }
    public Vector2[] ReturnObstacle2Code()
    {
        return doubleJumpObstacleVec;
    }
    public Vector2[] ReturnObstacle3Code()
    {
        return slideObstacleVec;
    }

    int GetRandomCode()
    {
        int code = UnityEngine.Random.Range(0, prefebsStruct.Length);
        return code;
    }
}
