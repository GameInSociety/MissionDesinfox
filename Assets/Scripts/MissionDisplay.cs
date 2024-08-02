using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionDisplay : Displayable
{
    public static MissionDisplay instance;

    public DisplayLevel[] displayLevels;

    public DisplayLevel currentLevel;

    public Displayable score_Displayable;

    public TextMeshProUGUI correctResponses_Text;
    public TextMeshProUGUI timeSpent_text;

    public GameObject goodFeedback_Obj;
    public GameObject badFeedback_Obj;
    public DisplayScore displayScore;

    public Sprite[] character_Sprites;


    public Image character_Image;
    public RectTransform character_RectTransform;

    /// <summary>
    /// LIVES
    /// </summary>
    public Color[] lives_Colors;
    public Image lives_Outline;
    public Image[] lives_Images;
    public int lives = 5;

    public int maxLives = 5;

    private void Awake() {
        instance = this;
    }

    public void DisplayMission() {

        foreach (var displayLevel in displayLevels) {
            displayLevel.Hide();
        }
        FadeIn();

        Level level = LevelManager.Instance.currentLevel;
        currentLevel = displayLevels[(int)level.type];
        displayLevels[(int)level.type].StartLevel();
        lives_Outline.color = lives_Colors[(int)level.type];

        lives = maxLives;

        UpdateCharacter();
    }

    public void ShowScore(float time, int correctAnsers, int totalAnswers) {
        score_Displayable.FadeIn();

        int time_i = Mathf.RoundToInt(time);
        timeSpent_text.text = $"{time_i} secondes";

        correctResponses_Text.text = $"{correctAnsers} / {totalAnswers}";

        float lerp = (float)correctAnsers / totalAnswers;
        int score = (int)lerp * 3;
        Debug.Log(score);
        displayScore.UpdateScore(score);
    }

    public void Document_Sucess() {

        DisplayDialogue.Instance.Display($"Bravo !\n{currentLevel.GetCurrentDocument().explanation}");

        goodFeedback_Obj.SetActive(true);

        UpdateCharacter();
        DisplayDialogue.Instance.onClose += currentLevel.NextDocument;


    }
    public void Document_Fail() {
        DisplayDialogue.Instance.Display($"Rat� !\n{currentLevel.GetCurrentDocument().explanation}");
        badFeedback_Obj.SetActive(true);
        --lives;

        UpdateCharacter();

        DisplayDialogue.Instance.onClose += currentLevel.NextDocument;
    }


    public void Help() {
        DisplayDialogue.Instance.Display(currentLevel.GetCurrentDocument().clue);
    }
    

    void UpdateCharacter() {
        for (int j = 0; j < lives_Images.Length; j++) {
            lives_Images[j].color = lives > j ? Color.white : Color.clear;
        }
        Tween.Bounce(character_RectTransform);
        int i = Mathf.Clamp(lives, 0, 5);
        character_Image.sprite = character_Sprites[5-i];
    }

    public void ExitLevel() {
        Level level = LevelManager.Instance.currentLevel;
        displayLevels[(int)level.type].EndLevel();
        FadeOut();
        SelectionMenu.Instance.FadeIn();
    }
}
