using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayButton : Displayable, IPointerClickHandler
{   
    public Displayable showTarget;
    public Displayable hideTarget;

    public bool locked = false;

    public bool lockOnEnable = false;
    public bool playsound = false;
    void OnEnable()
    {
        if (lockOnEnable)
        {
            Lock();
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (locked)
        {
            return;
        }

        SoundManager.Instance.PlaySound("Click");

        Trigger();   
    }
    
    public void Trigger()
    {
        if (playsound) {
            SoundManager.Instance.music_Source.Play();
        }

        Tween.Bounce(GetTransform);

        showTarget?.FadeIn();
        hideTarget?.FadeOut();
    }

    public void Lock()
    {
        CanvasGroup.alpha = 0.5f;
        locked = true;
    }

    public void Unlock()
    {
        CanvasGroup.alpha = 1f;

        locked = false;
    }
}
