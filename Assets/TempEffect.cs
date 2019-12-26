using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TempEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
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
        throw new System.NotImplementedException();
    }

    public void Setup(DisplayMod parMod)
    {
        mod = parMod;
    }
}