using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level 
{
    public enum Type {
        FakeInfo,
        OpVsInfo,
        Deminage,
        Bias
    }


    public string name = "";

    public List<Document> documents = new List<Document>();

    public Type type = Type.FakeInfo;

    public string description = "";
}
