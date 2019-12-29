using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    protected TextMeshProUGUI text = null;

    [SerializeField]
    protected Image icon = null;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}

public class TempEffect : BaseEffect
{
    private DisplayMod mod;

    public override void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(mod.Duration);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
    }

    public void Setup(DisplayMod parMod)
    {
        mod = parMod;
        DisplayTimeLeft();
        DateSystem.NewHourEvent += DisplayTimeLeft;
    }

    private void OnDestroy()
    {
        DateSystem.NewHourEvent -= DisplayTimeLeft;
    }

    private void DisplayTimeLeft()
    {
        text.text = $"{mod.Duration}";
    }
}