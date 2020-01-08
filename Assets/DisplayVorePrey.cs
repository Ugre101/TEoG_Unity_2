using TMPro;
using UnityEngine;

namespace Vore
{
    public enum VoreContainers
    {
        Stomach,
        Anal,
        Vagina,
        Balls,
        Boobs
    }

    public class DisplayVorePrey : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI title = null, desc = null;

        private ThePrey prey;
        private VoreContainers voreContainers;

        public void Setup(ThePrey prey, VoreContainers voreContainers)
        {
            this.prey = prey;
            this.voreContainers = voreContainers;
            DisplayPrey();
        }

        private void DisplayPrey()
        {
            title.text = voreContainers.ToString();
            string preyDesc = $"{prey.Prey.Identity.FullName}\n\nWeight: {prey.Prey.Weight}";
            desc.text = preyDesc;
        }
    }
}