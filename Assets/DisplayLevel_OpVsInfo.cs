using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLevel_OpVsInfo : DisplayLevel
{
    public Image[] images;

    public int correctImage = 0;

    bool canPlay = false;


    public void PressImage(int i) {
        if ( correctImage == i) {
            ++correctAnswers;
            MissionDisplay.instance.DisplayGoodFeedback();
        } else {
            MissionDisplay.instance.DisplayBadFeedback();
        }

        Invoke($"NextDocument", 2f);
        Debug.Log($"Press {i}");
        canPlay = false;
    }

    public override void UpdateCurrentDocument() {
        base.UpdateCurrentDocument();
        canPlay = true;
    }

    public override void UpdateImage() {
        //base.UpdateImage();
        var sprite = GetCurrentDocument().GetSprite();
        
        foreach ( var item in images ) {
            item.sprite = sprite;
            item.transform.localScale = Vector3.zero;
        }
    }

    public override void ShowImage() {
        //base.ShowImage();
        foreach ( var item in images ) {
            Tween.Bounce(item.rectTransform);
        }
    }
}
