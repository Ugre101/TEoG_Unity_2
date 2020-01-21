using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EssenceMenu
{
    public abstract class GrowOrgan : MonoBehaviour
    {
        [SerializeField] protected Button btn = null;
        [SerializeField] protected TextMeshProUGUI btnText = null;
        protected PlayerMain player;

        protected void BaseSetup(PlayerMain playerMain)
        {
            this.player = playerMain;
            DisplayCost();
            btn.onClick.AddListener(Grow);
        }

        protected abstract void DisplayCost();

        protected abstract void Grow();
    }
}