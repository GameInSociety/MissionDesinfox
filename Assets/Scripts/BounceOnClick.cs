using UnityEngine.EventSystems;

public class BounceOnClick : Displayable, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        SoundManager.Instance.PlaySound("Click");
        Tween.Bounce(GetTransform);
    }

}