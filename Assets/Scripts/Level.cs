using System.Collections.Generic;

[System.Serializable]
public class Level 
{
    public enum Type {
        FakeInfo,
        OpVsInfo,
        Biais,
        QuoiCroire
    }


    public string name = "";

    public bool finished = false;

    public List<Document> documents = new List<Document>();

    public Type type = Type.FakeInfo;

    public string description = "";
}
