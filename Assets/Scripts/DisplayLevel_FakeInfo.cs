using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLevel_FakeInfo : DisplayLevel
{
    bool canPress = false;

    public Displayable button_Fake;
    public Displayable button_Info;

    public override void StartLevel() {
        base.StartLevel();

        button_Fake.Hide();
        button_Info.Hide();
    }

    public override void UpdateCurrentDocument() {
        base.UpdateCurrentDocument();
        button_Fake.FadeIn();
        button_Info.FadeIn();
        Invoke("Delay", 0f);
    }
    public void PressInfo() {
        if (!GetCurrentDocument().fake) {
            ++correctAnswers;
            MissionDisplay.instance.DisplayGoodFeedback();
        } else {
            MissionDisplay.instance.DisplayBadFeedback();

        }

        button_Fake.FadeOut();
        Press();
    }

    public void Press() {
        targetImage.DOColor(Color.clear, 0.5f);
        canPress = false;
        Invoke("NextDocument", 1f);
    }

    public void PressFake() {
        if (GetCurrentDocument().fake) {
            ++correctAnswers;
            MissionDisplay.instance.DisplayGoodFeedback();
        } else {
            MissionDisplay.instance.DisplayBadFeedback();
        }

        button_Info.FadeOut();
        Press();
        
    }

}
