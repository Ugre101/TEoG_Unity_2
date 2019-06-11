using UnityEngine;
using UnityEngine.EventSystems;

public class QuestHoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject container;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (container != null)
        {
            container.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (container != null)
        {
            container.SetActive(false);
        }
    }
}
