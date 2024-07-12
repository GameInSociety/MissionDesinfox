using System.Collections;
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

    private void Update() {
        if ( Input.GetKeyDown(KeyCode.L))
        {
            var texture = Resources.Load(path) as Texture2D;
            image.sprite = Sprite.Create(texture, new Rect (0f, 0f, texture.width, texture.height), Vector2.zero);
            if (texture != null) {
                Debug.Log($"found:{path} {texture.GetType().Name}");
            } else {
                Debug.Log($"not found:{path}");
            }
        }
    }

}
