using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverText : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private TextMeshProUGUI _hoverText;

    private void Start()
    {
        // This code wont work on if there is more than one tmpugui child
        _hoverText = GetComponentInChildren<TextMeshProUGUI>();
        if (_hoverText != null)
        {
            _hoverText.enabled = false;
        }
    }

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