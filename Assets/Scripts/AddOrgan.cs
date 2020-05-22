using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EssenceMenu
{
    public abstract class AddOrgan : MonoBehaviour
    {
        [SerializeField] protected Button btn = null;
        [SerializeField] protected TextMeshProUGUI btnText = null;
        [SerializeField] protected Image image = null;
        [SerializeField] protected Color canAfford, cantAfford;
        protected PlayerMain player;
        protected abstract Essence Ess { get; }
        protected abstract float Cost { get; }
        protected bool CanAfford => Ess.Amount >= Cost;

        protected void ShowIfCanAfford() => image.color = CanAfford ? canAfford : cantAfford;

        public virtual Button Setup(PlayerMain player)
        {
            this.player = player;
            btn.onClick.AddListener(AddFunc);
            DisplayCost();
            image = image != null ? image : GetComponent<Image>();
            Ess.EssenceSliderEvent += ShowIfCanAfford;
            ShowIfCanAfford();
            return btn;
        }

        private void OnDestroy() => Ess.EssenceSliderEvent -= ShowIfCanAfford;

        protected abstract void DisplayCost();

        protected abstract void AddFunc();
    }
}