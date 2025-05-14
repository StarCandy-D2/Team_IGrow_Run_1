using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapDataJson;

public class zTestScript : MonoBehaviour
{
    public struct JellyStruct
    {
        public int Test { get; private set; }
        public Vector2[] JumpObsJellyVec { get; private set; }
        public Vector2[] DblObsJellyVec { get; private set; }
        public Vector2[] SlideObsJellyVec { get; private set; }
        public JellyStruct(int test, Vector2[] jumpobs, Vector2[] dblObs, Vector2[] slideObs)
        {
            Test = test; JumpObsJellyVec = jumpobs; DblObsJellyVec = dblObs; SlideObsJellyVec = slideObs;
        }
    }

    JellyStruct jellyStruct;
    void Start()
    {
        string jsonString2 = Resources.Load("Jelly").ToString();
        jellyStruct = JsonConvert.DeserializeObject<JellyStruct>(jsonString2);
        Debug.Log(jsonString2);
        Debug.Log(jellyStruct.Test);
        Debug.Log(jellyStruct.JumpObsJellyVec);
    }

}
