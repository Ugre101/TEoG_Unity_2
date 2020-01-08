using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI hoverText = null;

    private bool HoverEnabled { set => hoverText.gameObject.SetActive(value); }

    private void OnEnable()
    {
        if (hoverText == null)
        {
            enabled = false;
        }
        HoverEnabled = false; 
    }

    public void OnPointerEnter(PointerEventData eventData) => HoverEnabled = true;

    public void OnPointerExit(PointerEventData eventData) => HoverEnabled = false;
}