using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayLevel_Bias : DisplayLevel
{
    public static DisplayLevel_Bias Instance;

    public List<BiasButton> buttons = new List<BiasButton>();

    private void Awake() {
        Instance = this;
    }

    public void PressButton(int index) {

        string correctAnswer = GetCurrentDocument().correctStatement.ToLower();
        string chosenAnswer = buttons[index].GetComponentInChildren<TextMeshProUGUI>().text.ToLower();
        Debug.Log($"correct answer : {correctAnswer}");
        Debug.Log($"chosen answer : {chosenAnswer}");

        if (chosenAnswer == correctAnswer) {
            ++correctAnswers;
            MissionDisplay.instance.Document_Sucess();
        } else {
            MissionDisplay.instance.Document_Fail();
        }


        targetImage.DOColor(Color.clear, 0.5f);
    }

    public override void UpdateCurrentDocument() {
        base.UpdateCurrentDocument();

        targetImage.DOColor(Color.white, 0.5f);
    }
}
