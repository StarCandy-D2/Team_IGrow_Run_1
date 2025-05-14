using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using JetBrains.Annotations;


public class MapDataJson : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    public struct PrefebsStruct
    {
        public string Stage { get; private set; }
        public float[] Ground { get; private set; }
        public Vector2[] Platform { get; private set; }
        public float[] JumpObstacle { get; private set; }
        public float[] DoubleJumpObstacle { get; private set; }
        public float[] SlideObstacle { get; private set; }
        public int[][] JellySet { get; private set; }
        public PrefebsStruct(string stage, float[] ground, Vector2[] platform, float[] jumpObstacle, float[] doubleJumpObstacle, float[] slideObstacle, int[][] jellySet)
        {
            Stage = stage; Ground = ground; Platform = platform; JumpObstacle = jumpObstacle; DoubleJumpObstacle = doubleJumpObstacle; SlideObstacle = slideObstacle; JellySet = jellySet;
        }
    }

    public struct JellyStruct
    {
        public Vector2[] JumpObsJellyVec { get; private set; }
        public Vector2[] DblObsJellyVec { get; private set; }
        public Vector2[] SlideObsJellyVec { get; private set; }

        public JellyStruct(Vector2[] jumpObsJellyVec, Vector2[] dblObsJellyVec, Vector2[] slideObsJellyVec)
        {
            JumpObsJellyVec = jumpObsJellyVec; DblObsJellyVec = dblObsJellyVec; SlideObsJellyVec= slideObsJellyVec;


        }
    }

    PrefebsStruct[][] prefebsStruct;
    public JellyStruct jellyStruct;

    Vector2[] GroundVec;
    Vector2[] PlatformVec;
    Vector2[] jumpObstacleVec;
    Vector2[] doubleJumpObstacleVec;
    Vector2[] slideObstacleVec;
    int[] jellySetArr;

    public int mapCode = -1;
    public int stageCode = 0; // 순환 가능하게 변경
    private const int MaxStageCount = 3; // JSON에서 총 4개 스테이지 (0,1,2)
    private void Start()
    {
        string jsonString = Resources.Load("MapData").ToString();
        string jsonString2 = Resources.Load("Jelly").ToString();
        prefebsStruct = JsonConvert.DeserializeObject<PrefebsStruct[][]>(jsonString);
        jellyStruct = JsonConvert.DeserializeObject<JellyStruct>(jsonString2);

        GetCode();
    }
    public void GetCode()
    {
        // 튜토리얼이 끝나면 stageCode를 순환시킴
        if (!tutorialManager.isFirstRun)
        {
            stageCode = stageCode % MaxStageCount;
        }
        else
        {
            stageCode = 0; // 튜토리얼 중엔 고정
        }

        mapCode = GetRandomCode(mapCode);

        GetGroundCode();
        GetPlatformCode();
        GetJumpObstacleCode();
        GetDoubleJumpObstaclCode();
        GetSlideObstacleCode();
        GetJellySetCode();
    }

    public void AdvanceStage()
    {
        stageCode = (stageCode + 1) % MaxStageCount;
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
    void GetJellySetCode()
    {
        if (prefebsStruct[stageCode][mapCode].JellySet != null)
        {
            jellySetArr = new int[2];
            for (int i = 0; i < prefebsStruct[stageCode][mapCode].SlideObstacle.Length; i++)
            {
                jellySetArr = prefebsStruct[stageCode][mapCode].JellySet[i];
            }
        }
        else if (prefebsStruct[stageCode][mapCode].JellySet == null)
        {
            jellySetArr = null;
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
    public int[] ReturnJellySets()
    {
        return jellySetArr;
    }

    int GetRandomCode(int prevCode)
    {
        if (stageCode == 0 && tutorialManager.isFirstRun)
        {
            prevCode++;
            if (prevCode >= prefebsStruct[stageCode].Length)
            {
                tutorialManager.isFirstRun = false;
                return 0;
            }
            return prevCode;
        }
        else
        {
            int length = prefebsStruct[stageCode].Length;
            if (length == 0)
            {
                Debug.LogError($"[MapDataJson] No maps defined for stage {stageCode}");
                return 0;
            }

            return UnityEngine.Random.Range(0, length);
        }
    }
}
