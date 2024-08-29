using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public Sprite empty_sprite;
    public Sprite full_sprite;

    public Image[] images;

    private void Start() {
    }

    public void UpdateScore(int i) {
        for (int j = 0; j < images.Length; j++) {
            images[j].sprite = i > j ? full_sprite : empty_sprite;
        }
    }
}
