using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected TextMeshProUGUI text = null;

    [SerializeField] protected Image icon = null;
    protected GameUIHoverText hoverText = null;

    public abstract void OnPointerClick(PointerEventData eventData);

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);
}

public class TempEffect : BaseEffect
{
    private DisplayMod mod;

    public override void OnPointerClick(PointerEventData eventData)
    {
        hoverText.Hovering(mod.Source, mod.Desc());
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        hoverText.Hovering(mod.Source, mod.Desc());
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        hoverText.StopHovering();
    }

    public void Setup(DisplayMod parMod, GameUIHoverText hoverText)
    {
        this.hoverText = hoverText;
        mod = parMod;
        DisplayTimeLeft();
        DateSystem.NewHourEvent += DisplayTimeLeft;
    }

    private void OnDestroy()
    {
        DateSystem.NewHourEvent -= DisplayTimeLeft;
    }

    private void DisplayTimeLeft() => text.text = $"{mod.Duration}";
}