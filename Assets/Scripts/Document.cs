using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Document
{
    public string name = "";
    public string imageName = "";
    public string maskName = "";
    public string[] statements = new string[4] {
        "statement 1",
        "statement 2",
        "statement 3",
        "statement 4",
    };

    public string text = "test text";
    public string video_path = "video_test";
    public string audio_path = "audio_test";

    public List<Category> categories = new List<Category>();
    public bool fake = false;

    public Sprite GetSprite() {
        string path = $"Images/Sources/{imageName}";
        var sprite = Resources.Load<Sprite>(path);

        if (sprite == null) {
            Debug.LogError($"no texture for document {name} / image : {imageName}");
            return null;
        }

        return sprite;
    }

    public Sprite GetMask() {
        string path = $"Images/Masks/{imageName}m";
        var sprite = Resources.Load<Sprite>(path);

        if (sprite == null) {
            Debug.LogError($"no texture for document {name} / image : {imageName}");
            return null;
        }


        return sprite;
    }
}

