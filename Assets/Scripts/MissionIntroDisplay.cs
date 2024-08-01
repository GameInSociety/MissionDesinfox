using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionIntroDisplay : Displayable
{
    public static MissionIntroDisplay Instance;

    public Sprite[] bg_sprites;
    public Sprite[] rect_sprites;
    public Sprite[] title_sprites;

    public Image bg_image;
    public Image rect_image;
    public Image title_image;

    public TextMeshProUGUI title_text;
    public TextMeshProUGUI description_text;
    public DisplayScore displayScore;

    private void Awake() {
        Instance = this;
    }

    public void UpdateUI() {
        Level level = LevelManager.Instance.currentLevel;
        int i = (int)level.type;

        bg_image.sprite = bg_sprites[i];
        rect_image.sprite = rect_sprites[i];
        title_image.sprite = title_sprites[i];
    }

    public void StartMission() {
        MissionIntroDisplay.Instance.FadeOut();
        MissionDisplay.instance.DisplayMission();

        
    }
}
