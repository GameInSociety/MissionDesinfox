using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Document
{
    // general
    public List<string> types = new List<string>();
    public List<string> medias = new List<string>();


    public string clue = "";
    public string explanation_Good = "";
    public string explanation_Bad = "";


    // fnof
    public List<string> colorNames = new List<string>();
    public bool fake = false;
    public List<string> interactibleElements = new List<string>();
    public List<string> statements = new List<string>();

    // LBC & FHO
    public string correctStatement = "";
}

