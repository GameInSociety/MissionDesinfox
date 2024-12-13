using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class Video_ScrubBar : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {
    public RectTransform knob_RectTransform;
    public RectTransform bg_RectTransform;
    public RectTransform fill_RectTransform;

    public Slider soundSlider;

    public RectTransform startAnchor;
    public RectTransform endAnchor;

    public VideoPlayer player;

    public float lerp;

    bool dragging = false;

    private void Update() {
        if (dragging) {

        } else {
            float lerp = (float)player.time / (float)player.length;
            float x = lerp * bg_RectTransform.sizeDelta.x;
            fill_RectTransform.sizeDelta = new Vector2(x, fill_RectTransform.sizeDelta.y);
        }

        player.SetDirectAudioVolume(0, soundSlider.value);
    }

    public void OnDrag(PointerEventData eventData) {
        dragging = true;
        UpdateVideoTime(eventData.pointerCurrentRaycast.worldPosition);
    }

    public void OnPointerDown(PointerEventData eventData) {
        UpdateVideoTime(eventData.pointerCurrentRaycast.worldPosition);
    }

    public void UpdateVideoTime(Vector3 pos) {
        if (player.isPlaying) {
            player.Pause();
        }
        float d = Vector3.Distance(startAnchor.position, endAnchor.position);
        pos.y = startAnchor.position.y;
        float f = Vector3.Distance(startAnchor.position, pos);
        lerp = f / d;
        lerp = Mathf.Clamp01(lerp);
        float x = lerp * bg_RectTransform.sizeDelta.x;
        fill_RectTransform.sizeDelta = new Vector2(x, fill_RectTransform.sizeDelta.y);
        player.time = player.length * lerp;
    }

    public void OnPointerUp(PointerEventData eventData) {
        dragging = false;
        player.Play();
    }

}
