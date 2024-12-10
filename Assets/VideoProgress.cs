using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class VideoProgress : MonoBehaviour
{
    public RectTransform bg;
    public RectTransform fill;


    public float test;
    public VideoPlayer player;

    public void LateUpdate() {
        float t = bg.sizeDelta.x;
        float lerp = Mathf.Clamp01((float)player.time / (float)player.length);
        Debug.Log($"w : {t}");
        float w = lerp * t;
        Debug.Log($"w2 : {w}");
        fill.sizeDelta = new Vector2(w, fill.sizeDelta.y);
    }
}
