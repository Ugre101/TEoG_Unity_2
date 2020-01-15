using UnityEngine;
using UnityEngine.EventSystems;

public class TempVore : BaseEffect
{
    public DisplayVore DisplayVore { get; private set; }

    public void Setup(DisplayVore parVore) => DisplayVore = parVore;

    private void Update()
    {
        icon.fillAmount = 1f - DisplayVore.Progress();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(DisplayVore.Progress());
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