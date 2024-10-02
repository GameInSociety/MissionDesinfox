using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using JetBrains.Annotations;

public class MissionIntroDisplay : Displayable
{
    public static MissionIntroDisplay Instance;

    public List<string> missionIntroductions = new List<string>();
    public List<string> biaisTitles= new List<string>();
    public List<string> biaisDefs= new List<string>();
    public string gameIntroduction;
    public string gameConclusion;

    public Sprite[] bg_sprites;
    public Sprite[] rect_sprites;
    public Sprite[] title_sprites;
    public Sprite[] play_sprites;

    public Image bg_image;
    public Image rect_image;
    public Image title_image;
    public Image play_image;

    public ScrollRect scrollRect;

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
        play_image.sprite = play_sprites[i];

        scrollRect.verticalNormalizedPosition = 0f;

        description_text.text = missionIntroductions[i];
    }

    public void StartMission() {
        MissionIntroDisplay.Instance.FadeOut();
        MissionDisplay.instance.DisplayMission();

        
    }
}
