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

        private ThePrey thePrey;
        private BasicChar prey => thePrey.Prey;
        private VoreContainers voreContainers;

        public Button Setup(ThePrey thePrey, VoreContainers voreContainers)
        {
            this.thePrey = thePrey;
            this.voreContainers = voreContainers;
            DisplayPrey();
            return btn = btn != null ? btn : GetComponent<Button>();
        }

        private void DisplayPrey()
        {
            title.text = voreContainers.ToString();
            string preyDesc = $"{prey.Identity.FullName}\n\nWeight: {prey.Body.WeightKgOrP()}";
            desc.text = preyDesc;
        }
    }
}