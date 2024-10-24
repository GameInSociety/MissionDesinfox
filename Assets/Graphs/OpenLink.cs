using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenLink : MonoBehaviour, IPointerClickHandler {
    public string url = "";
    public void OnPointerClick(PointerEventData eventData) {
        Tween.Bounce(transform);
        Application.OpenURL(url);
    }
}
