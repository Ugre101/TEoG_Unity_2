using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EssenceMenuStuff
{
    public abstract class AddOrgan : MonoBehaviour
    {
        [SerializeField] protected Button btn = null;
        [SerializeField] protected TextMeshProUGUI btnText = null;
        [SerializeField] protected Image image = null;
        [SerializeField] protected Color canAfford, cantAfford;
        protected BasicChar Player => PlayerMain.Player;
        protected abstract OrganContainer OrganContainer { get; }
        protected abstract Essence Ess { get; }
        protected float Cost => OrganContainer.AddCost;
        protected bool CanAfford => Ess.Amount >= Cost;

        protected void ShowIfCanAfford() => image.color = CanAfford ? canAfford : cantAfford;

        public virtual Button Setup()
        {
            btn.onClick.AddListener(AddFunc);
            DisplayCost();
            image = image != null ? image : GetComponent<Image>();
            Ess.ChangeEvent += ShowIfCanAfford;
            ShowIfCanAfford();
            return btn;
        }

        private void OnDestroy() => Ess.ChangeEvent -= ShowIfCanAfford;

        protected abstract void DisplayCost();

        protected void AddFunc()
        {
            if (CanAfford)
            {
                Ess.Lose(Cost);
                OrganContainer.AddNew();
                DisplayCost();
                ShowIfCanAfford();
            }
        }
    }
}