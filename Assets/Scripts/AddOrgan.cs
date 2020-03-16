using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EssenceMenu
{
    public abstract class AddOrgan : MonoBehaviour
    {
        [SerializeField] protected Button btn = null;
        [SerializeField] protected TextMeshProUGUI btnText = null;
        protected PlayerMain player;

        public virtual Button Setup(PlayerMain player)
        {
            this.player = player;
            btn.onClick.AddListener(AddFunc);
            DisplayCost();
            return btn;
        }

        protected abstract void DisplayCost();

        protected abstract void AddFunc();

        public delegate void AddedOrgan();

        public event AddedOrgan Added;
    }
}