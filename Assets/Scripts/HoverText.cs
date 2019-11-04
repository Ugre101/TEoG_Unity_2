using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI _hoverText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_hoverText != null)
        {
            _hoverText.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_hoverText != null)
        {
            _hoverText.enabled = false;
        }
    }
}