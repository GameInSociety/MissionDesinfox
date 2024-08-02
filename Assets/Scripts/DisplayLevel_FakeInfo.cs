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
    }

    public override void UpdateCurrentDocument() {
        base.UpdateCurrentDocument();
        button_Fake.FadeIn();
        button_Info.FadeIn();
    }
    public void PressInfo() {
        if (!GetCurrentDocument().fake) {
            ++correctAnswers;
            MissionDisplay.instance.Document_Sucess();
        } else {
            MissionDisplay.instance.Document_Fail();

        }

        button_Fake.FadeOut();
        Press();
    }

    public void Press() {
        targetImage.DOColor(Color.clear, 0.5f);
        canPress = false;
    }

    public void PressFake() {
        if (GetCurrentDocument().fake) {
            ++correctAnswers;
            MissionDisplay.instance.Document_Sucess();
        } else {
            MissionDisplay.instance.Document_Fail();
        }

        button_Info.FadeOut();
        Press();
        
    }

}
