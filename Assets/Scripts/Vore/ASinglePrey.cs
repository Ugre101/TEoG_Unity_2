using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vore
{
    public class ASinglePrey : MonoBehaviour
    {
        private BasicChar player = null;
        [SerializeField] private CharHolder preyHolder = null;
        [SerializeField] private TextMeshProUGUI title = null, desc = null;
        [SerializeField] private Button backBtn = null, reguBtn = null;
        [SerializeField] private Transform optionContainer = null, enemyContainer = null;
        [SerializeField] private VoreMenuHandler voreMenu = null;
        private VoreContainers voreContainer = VoreContainers.Stomach;
        private ThePrey prey = null;

        private float DigestionProgress => prey.Progress;

        private void Start()
        {
            player = player ?? PlayerMain.Player;
            backBtn.onClick.AddListener(Back);
            reguBtn.onClick.AddListener(Regurgileta);
        }

        public void Setup(ThePrey prey, VoreContainers voreContainer)
        {
            gameObject.SetActive(true);
            this.prey = prey;
            this.voreContainer = voreContainer;
            title.text = prey.Prey.Identity.FullName;
            desc.text = prey.PreyDesc;
        }

        private void Regurgileta()
        {
            // TODO add instate
            if (DigestionProgress < 0.5)
            {
                Vector3 middleOfMap = MapEvents.CurrentMap.cellBounds.center;
                Instantiate(preyHolder, middleOfMap, Quaternion.identity, enemyContainer).Load(prey.Prey);
                player.Vore.GetVoreOrgan(voreContainer).RemovePrey(prey);
                Back();
            }
            else
            {
                player.Vore.GetVoreOrgan(voreContainer).RemovePrey(prey);
                Back();
                // Dead
            }
        }

        private void Back() => voreMenu.OnEnable();
    }
}