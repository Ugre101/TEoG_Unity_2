using UnityEngine;
using UnityEngine.EventSystems;

public class TempVore : BaseEffect
{
    public DisplayVore displayVore { get; private set; }

    public void Setup(DisplayVore parVore) => displayVore = parVore;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(displayVore.Progress());
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }
}