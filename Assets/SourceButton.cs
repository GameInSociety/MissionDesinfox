using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SourceButton : Displayable, IPointerClickHandler {
    public DisplayLevel_QuoiCroire.SourceType type;
    public int index;
    public GameObject[] groups;

    public void Display(int i, DisplayLevel_QuoiCroire.SourceType type) {
        this.type = type;
        index = i;
        FadeIn();

        foreach (GameObject group in groups) {
            group.SetActive(false);
        }
        groups[(int)type].gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData) {
        Tween.Bounce(GetTransform);
        string source = DisplayLevel_QuoiCroire.Instance.GetCurrentDocument().sources[index];
        DisplayLevel_QuoiCroire.Instance.ShowSource(source, type);

        Debug.Log($"clicked {type}");
    }
}
