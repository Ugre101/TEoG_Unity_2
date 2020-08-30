using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EssenceMenuStuff
{
    public abstract class GrowOrgan : MonoBehaviour
    {
        [SerializeField] protected Button btn = null;
        [SerializeField] protected TextMeshProUGUI btnText = null;
        [SerializeField] protected Image image = null;
        [SerializeField] protected Color canAfford = new Color(), cantAfford = Color.red;
        protected BasicChar Player => PlayerMain.Player;
        protected abstract Essence Ess { get; }
        protected abstract float Cost { get; }
        protected bool CanAfford => Ess.Amount >= Cost;

        protected void ShowIfCanAfford() => image.color = CanAfford ? canAfford : cantAfford;

        protected virtual void BaseSetup()
        {
            DisplayCost();
            btn.onClick.AddListener(Grow);
            image = image != null ? image : GetComponent<Image>();
            Ess.ChangeEvent += ShowIfCanAfford;
            ShowIfCanAfford();
        }

        private void OnDestroy() => Ess.ChangeEvent -= ShowIfCanAfford;

        protected abstract void DisplayCost();

        protected abstract void Grow();
    }
}