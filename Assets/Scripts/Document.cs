using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Document
{
    // general
    public string name = "";
    public string imageName = "";
    public string maskName = "";
    public string clue = "";
    public string explanation = "";
    

    // fnof
    public bool fake = false;
    public List<string> interactibleElements = new List<string>();

    // quoi croire
    public List<string> sources = new List<string>();
    public List<string> statements = new List<string>();

    // LBC & FHO
    public string correctStatement = "";

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
        string path = $"Images/Masks/{maskName}";
        var sprite = Resources.Load<Sprite>(path);

        if (sprite == null) {
            Debug.LogError($"no texture for document {name} / image : {maskName}");
            return null;
        }


        return sprite;
    }
}

