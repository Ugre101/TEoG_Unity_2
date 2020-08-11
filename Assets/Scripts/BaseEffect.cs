using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ugre.GameUITempEffects
{
    public abstract class BaseEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] protected TextMeshProUGUI text = null;

        [SerializeField] protected Image icon = null;
        protected GameUIHoverText hoverText = null;

        public void OnPointerClick(PointerEventData eventData) => Hovering();

        public void OnPointerEnter(PointerEventData eventData) => Hovering();

        public void OnPointerExit(PointerEventData eventData) => hoverText.StopHovering();

        protected abstract void Hovering();
    }
}