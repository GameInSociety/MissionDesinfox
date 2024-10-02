using DG.Tweening;
using UnityEngine;

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
    }

    public override void OnMediaDownloaded() {
        base.OnMediaDownloaded();
        button_Fake.FadeIn();
        button_Info.FadeIn();
        button_Fake.GetComponentInChildren<Animator>().SetBool("bounce", false);
        button_Info.GetComponentInChildren<Animator>().SetBool("bounce", false);
        canPress = true;
    }
    public void PressInfo() {
        Debug.Log($"pressing in fo");
        if (!canPress) {
            return;
        }
        if (GetCurrentDocument().fake) {
            MissionDisplay.instance.Document_Fail();
        } else {
            ++correctAnswers;
            MissionDisplay.instance.Document_Sucess();
        }

        button_Info.GetComponentInChildren<Animator>().SetBool("bounce", true);
        button_Fake.FadeOut();
        Press();
    }

    public void Press() {
        canPress = false;
    }

    public void PressFake() {
        Debug.Log($"pressing fake");
        if (!canPress) {
            return;
        }
        if (GetCurrentDocument().fake) {
            ++correctAnswers;
            MissionDisplay.instance.Document_Sucess();
        } else {
            MissionDisplay.instance.Document_Fail();
        }

        button_Info.FadeOut();
        button_Fake.GetComponentInChildren<Animator>().SetBool("bounce", true);
        Press();
        
    }

}
