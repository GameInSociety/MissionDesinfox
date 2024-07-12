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

