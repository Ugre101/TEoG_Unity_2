using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vore
{
    public class DisplayVorePrey : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI title = null, desc = null;

        [SerializeField]
        private Button btn = null;

        private ThePrey prey;
        private VoreContainers voreContainers;

        public Button Setup(ThePrey prey, VoreContainers voreContainers)
        {
            this.prey = prey;
            this.voreContainers = voreContainers;
            DisplayPrey();
            return btn = btn != null ? btn : GetComponent<Button>();
        }

        private void DisplayPrey()
        {
            title.text = voreContainers.ToString();
            string preyDesc = $"{prey.Prey.Identity.FullName}\n\nWeight: {prey.Prey.Weight}";
            desc.text = preyDesc;
        }
    }
}