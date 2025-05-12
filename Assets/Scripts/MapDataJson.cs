using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using JetBrains.Annotations;
using System.IO;


public class MapDataJson : MonoBehaviour
{
    public struct PrefebsStruct
    {
        public string Stage { get; private set; }
        public float[] Ground { get; private set; }
        public Vector2[] Platform { get; private set; }
        public float[] JumpObstacle { get; private set; }
        public float[] DoubleJumpObstacle { get; private set; }
        public float[] SlideObstacle { get; private set; }
        public PrefebsStruct(string stage, float[] ground, Vector2[] platform, float[] jumpObstacle, float[] doubleJumpObstacle, float[] slideObstacle)
        {
            Stage = stage; Ground = ground; Platform = platform; JumpObstacle = jumpObstacle; DoubleJumpObstacle = doubleJumpObstacle; SlideObstacle = slideObstacle;
        }
    }

    PrefebsStruct[][] prefebsStruct;

    Vector2[] GroundVec;
    Vector2[] PlatformVec;
    Vector2[] jumpObstacleVec;
    Vector2[] doubleJumpObstacleVec;
    Vector2[] slideObstacleVec;

    int mapCode;
    int stageCode;
    private void Start()
    {
        string jsonString = Resources.Load("MapData").ToString();
        prefebsStruct = JsonConvert.DeserializeObject<PrefebsStruct[][]>(jsonString);

        GetCode();
    }
    public void GetCode()
    {
        mapCode = GetRandomCode();
        stageCode = Mathf.Clamp(GameManager.Instance.stage -1, 0, 2);

        GetGroundCode();
        GetPlatformCode();
        GetJumpObstacleCode();
        GetDoubleJumpObstaclCode();
        GetSlideObstacleCode();
    }
    void GetGroundCode()
    {
        if (prefebsStruct[stageCode][mapCode].Ground != null)
        {
            GroundVec = new Vector2[prefebsStruct[stageCode][mapCode].Ground.Length];
            for (int i = 0; i < prefebsStruct[stageCode][mapCode].Ground.Length; i++)
            {
                GroundVec[i] = new Vector2(prefebsStruct[stageCode][mapCode].Ground[i], 0f);
            }
        }
        else if (prefebsStruct[stageCode][mapCode].Ground == null)
        {
            GroundVec = null;
        }
    }
    void GetPlatformCode()
    {
        if (prefebsStruct[stageCode][mapCode].Platform != null)
        {
            PlatformVec = prefebsStruct[stageCode][mapCode].Platform;
        }
        else if (prefebsStruct[stageCode][mapCode].Platform == null)
        {
            PlatformVec = null;
        }
    }

    void GetJumpObstacleCode()
    {
        if (prefebsStruct[stageCode][mapCode].JumpObstacle != null)
        {
            jumpObstacleVec = new Vector2[prefebsStruct[stageCode][mapCode].JumpObstacle.Length];
            for (int i = 0; i < prefebsStruct[stageCode][mapCode].JumpObstacle.Length; i++)
            {
                jumpObstacleVec[i] = new Vector2(prefebsStruct[stageCode][mapCode].JumpObstacle[i], 1f);
            }
        }
        else if (prefebsStruct[stageCode][mapCode].JumpObstacle == null)
        {
            jumpObstacleVec = null;
        }

    }

    void GetDoubleJumpObstaclCode()
    {       
        if (prefebsStruct[stageCode][mapCode].DoubleJumpObstacle != null)
        {
            doubleJumpObstacleVec = new Vector2[prefebsStruct[stageCode][mapCode].DoubleJumpObstacle.Length];
            for (int i = 0; i < prefebsStruct[stageCode][mapCode].DoubleJumpObstacle.Length; i++)
            {
                doubleJumpObstacleVec[i] = new Vector2(prefebsStruct[stageCode][mapCode].DoubleJumpObstacle[i], 1.5f);
            }
        }
        else if (prefebsStruct[stageCode][mapCode].DoubleJumpObstacle == null)
        {
            doubleJumpObstacleVec = null;
        }
    }

    void GetSlideObstacleCode()
    {
        if (prefebsStruct[stageCode][mapCode].SlideObstacle != null)
        {
            slideObstacleVec = new Vector2[prefebsStruct[stageCode][mapCode].SlideObstacle.Length];
            for (int i = 0; i < prefebsStruct[stageCode][mapCode].SlideObstacle.Length; i++)
            {
                slideObstacleVec[i] = new Vector2(prefebsStruct[stageCode][mapCode].SlideObstacle[i], 4.9f);
            }
        }
        else if (prefebsStruct[stageCode][mapCode].SlideObstacle == null)
        {
            slideObstacleVec = null;
        }
    }

    public Vector2[] ReturnGroundCode()
    {
        return GroundVec;
    }
    public Vector2[] ReturnPlatformCode()
    {
        return PlatformVec;
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
        int code = UnityEngine.Random.Range(0, prefebsStruct[stageCode].Length);
        return code;
    }
}
