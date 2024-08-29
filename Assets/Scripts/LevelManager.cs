using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public List<Level> levels = new List<Level>();  
    public Level currentLevel = null;
    public Image image;
    public string path = "Images/Sources/chap01_01";

    private void Awake() {
        Instance = this;
    }


}
