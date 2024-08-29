using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLoading : Displayable
{
    public static DisplayLoading Instance;

    public RectTransform bg;
    public RectTransform fill;

    private void Awake() {
        Instance = this;
    }

    public void Reset() {
        fill.sizeDelta = new Vector2(0f, bg.sizeDelta.y);
    }

    public void Push(float lerp) {
        float w = lerp * bg.sizeDelta.x;
        fill.sizeDelta = new Vector2(Mathf.Clamp(w, 0, bg.sizeDelta.x), bg.sizeDelta.y);
    }

    public void End() {
        fill.sizeDelta = bg.sizeDelta;
    }
}
