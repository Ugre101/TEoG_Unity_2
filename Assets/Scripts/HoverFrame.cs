using UnityEngine;
using UnityEngine.EventSystems;

public class HoverFrame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject frame;

    public void OnPointerEnter(PointerEventData eventData) => frame.SetActive(true);

    public void OnPointerExit(PointerEventData eventData) => frame.SetActive(false);

    private void OnEnable()
    {
        if (frame == null)
            enabled = false;
        else
            frame.SetActive(false);
    }
}