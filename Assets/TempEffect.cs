using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TempEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI text = null;

    [SerializeField]
    private Image icon = null;

    private DisplayMod mod;

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(mod.Duration);
    }

    public void OnPointerExit(PointerEventData eventData)
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