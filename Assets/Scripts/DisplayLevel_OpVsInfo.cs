using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLevel_OpVsInfo : DisplayLevel
{
    public Image[] images;

    public int correctImage = 0;

    bool canInteract = false;


    public void PressImage(int index) {
        if (!canInteract) {
            return;
        }

        if ( correctImage == index) {
            ++correctAnswers;
            MissionDisplay.instance.DisplayGoodFeedback();
        } else {
            MissionDisplay.instance.DisplayBadFeedback();
        }

        Invoke($"NextDocument", 2f);
        Debug.Log($"Press {index}");
        canInteract = false;

        for (int i = 0; i < images.Length; i++) {
            if ( i == index)
                images[i].DOColor(Color.clear, 0.5f).SetDelay(1f);
            else
                images[i].DOColor(Color.clear, 0.5f);

        }
    }

    public override void UpdateCurrentDocument() {
        base.UpdateCurrentDocument();
        canInteract = true;
        foreach (var item in images) {
            item.DOColor(Color.white, 0.5f);
        }
    }

    public override void UpdateImage() {
        //base.UpdateImage();
        var sprite = GetCurrentDocument().GetSprite();
        
        foreach ( var item in images ) {
            item.sprite = sprite;
        }
    }

    public override void ShowImage() {
        //base.ShowImage();
    }
}
