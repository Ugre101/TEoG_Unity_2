using UnityEngine.EventSystems;

public class TempVore : BaseEffect
{
    public DisplayVore DisplayVore { get; private set; }

    public void Setup(DisplayVore parVore, GameUIHoverText hoverText)
    {
        this.hoverText = hoverText;
        DisplayVore = parVore;
    }

    private void Update() => icon.fillAmount = 1f - DisplayVore.Progress();

    private void Hovering() => hoverText.Hovering(DisplayVore.VoreOrgan.ToString(), DisplayVore.Progress().ToString());

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Hovering();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Hovering();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        hoverText.StopHovering();
    }
}