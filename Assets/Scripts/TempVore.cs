using UnityEngine;
using UnityEngine.EventSystems;

public class TempVore : MonoBehaviour, IPointerEnterHandler
{
    public DisplayVore displayVore { get; private set; }

    public void Setup(DisplayVore parVore) => displayVore = parVore;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(displayVore.Progress());
    }
}