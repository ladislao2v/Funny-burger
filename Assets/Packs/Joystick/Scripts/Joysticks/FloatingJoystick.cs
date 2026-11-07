using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    private const float IdleHeight = 0.3f;
    
    private bool _isIdleShown;

    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(_isIdleShown);
    }

    public override void ShowIdle()
    {
        Rect area = ((RectTransform)transform).rect;
        
        _isIdleShown = true;
        background.anchoredPosition = new Vector2(area.width * 0.5f, area.height * IdleHeight);
        background.gameObject.SetActive(true);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _isIdleShown = false;
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}