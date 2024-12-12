using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class Video_ScrubBar : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {
    public RectTransform knob_RectTransform;
    public RectTransform bg_RectTransform;
    public RectTransform fill_RectTransform;

    public float time = 0f;

    public void OnDrag(PointerEventData eventData) {
        
    }

    public void OnPointerDown(PointerEventData eventData) {
    }

    public void OnPointerUp(PointerEventData eventData) {
    }

}
