using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLevel_Bias : DisplayLevel
{
    public static DisplayLevel_Bias Instance;

    public List<BiasButton> buttons = new List<BiasButton>();

    private void Awake() {
        Instance = this;
    }

    public override void StartLevel() {
        base.StartLevel();
    }

    public void PressButton(int index) {
        if (index == 0) {
            ++correctAnswers;
            MissionDisplay.instance.DisplayGoodFeedback();
        } else {
            MissionDisplay.instance.DisplayBadFeedback();
        }

        foreach (var button in buttons) {
            button.FadeOut();
        }

        targetImage.DOColor(Color.clear, 0.5f);

        Invoke("NextDocument", 1f);
    }

    public override void UpdateCurrentDocument() {
        base.UpdateCurrentDocument();

        targetImage.DOColor(Color.white, 0.5f);

        Debug.Log($"categories count : {GetCurrentDocument().categories.Count}");
        foreach (var button in buttons) {
            button.Hide();
        }
        StartCoroutine(ShowButtonsCoroutine());
    }

    IEnumerator ShowButtonsCoroutine() {

        var categories = GetCurrentDocument().categories;
        for (int i = 0; i < categories.Count; i++) {

            buttons[i].Display(categories[i]);
            buttons[i].index = i;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
