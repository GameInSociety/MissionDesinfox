using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionDisplay : Displayable
{
    public static MissionDisplay instance;

    public DisplayLevel[] displayLevels;

    public Displayable score_Displayable;

    public TextMeshProUGUI correctResponses_Text;
    public TextMeshProUGUI timeSpent_text;

    public GameObject goodFeedback_Obj;
    public GameObject badFeedback_Obj;
    public DisplayScore displayScore;

    public Sprite[] character_Sprites;

    public Image character_Image;
    public RectTransform character_RectTransform;

    public Image[] lives_Images;
    public Sprite lives_empty;
    public Sprite lives_full;

    public int lives = 3;

    private void Awake() {
        instance = this;
    }

    public void DisplayMission() {

        foreach (var displayLevel in displayLevels) {
            displayLevel.Hide();
        }
        FadeIn();

        Level level = LevelManager.Instance.currentLevel;
        displayLevels[(int)level.type].StartLevel();

        lives = 3;

        UpdateCharacter();
    }

    public void ShowScore(float time, int correctAnsers, int totalAnswers) {
        score_Displayable.FadeIn();

        int time_i = Mathf.RoundToInt(time);
        timeSpent_text.text = System.TimeSpan.FromSeconds(time_i).ToString("mm:ss");

        correctResponses_Text.text = $"{correctAnsers} / {totalAnswers}";

        float lerp = (float)correctAnsers / totalAnswers;
        int score = (int)lerp * 3;
        Debug.Log(score);
        displayScore.UpdateScore(score);
    }

    public void DisplayGoodFeedback() {
        goodFeedback_Obj.SetActive(true);

        for (int j = 0; j < lives_Images.Length; j++) {
            lives_Images[j].color = lives > j ? Color.white: Color.black;
        }

        UpdateCharacter();


    }

    public void DisplayBadFeedback() {
        badFeedback_Obj.SetActive(true);

        for (int j = 0; j < lives_Images.Length; j++) {
            lives_Images[j].color = lives > j ? Color.white : Color.black;
        }

        --lives;
        UpdateCharacter();   
    }

    void UpdateCharacter() {
        Tween.Bounce(character_RectTransform);
        int i = Mathf.Clamp(lives, 0, 5);
        character_Image.sprite = character_Sprites[i];
    }

    public void ExitLevel() {
        Level level = LevelManager.Instance.currentLevel;
        displayLevels[(int)level.type].EndLevel();
        FadeOut();
        SelectionMenu.Instance.FadeIn();
    }
}