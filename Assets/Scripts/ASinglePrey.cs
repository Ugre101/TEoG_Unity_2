using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vore
{
    public class ASinglePrey : MonoBehaviour
    {
        [SerializeField] private PlayerMain player = null;
        [SerializeField] private TextMeshProUGUI title = null, desc = null;
        [SerializeField] private Button backBtn = null, reguBtn = null;
        [SerializeField] private Transform optionContainer = null, enemyContainer = null;
        [SerializeField] private VoreMenuHandler voreMenu = null;
        private VoreContainers voreContainer = VoreContainers.Stomach;
        private ThePrey prey = null;
        private VoreBasic VoreOrgan => player.Vore.GetVoreOrgan(voreContainer);
        private string PreyFName => prey.Identity.FirstName;
        private Body PreyBody => prey.Body;
        private bool DigestActive => VoreOrgan.Digestion;
        private float DigestionProgress => prey.Progress;

        private void Start()
        {
            player = player != null ? player : PlayerHolder.Player;
            backBtn.onClick.AddListener(Back);
            reguBtn.onClick.AddListener(Regurgileta);
        }

        public void Setup(ThePrey prey, VoreContainers voreContainer)
        {
            gameObject.SetActive(true);
            this.prey = prey;
            this.voreContainer = voreContainer;
            title.text = prey.Identity.FullName;
            desc.text = prey.PreyDesc;
        }

        private void Regurgileta()
        {
            // TODO add instate
            if (DigestionProgress < 0.5f)
            {
                Vector3 middleOfMap = MapEvents.CurrentMap.cellBounds.center;

              //  player.VoreChar.GetVoreContainer(voreContainer).ReleasePreyTo(prey, enemyContainer, player.transform.position);
                player.Vore.GetVoreOrgan(voreContainer).RemovePrey(prey);
                Back();
            }
            else
            {
              //  player.VoreChar.GetVoreContainer(voreContainer).PreyIsdigested(prey);
                player.Vore.GetVoreOrgan(voreContainer).RemovePrey(prey);
                Back();
                // Dead
            }
        }

        private void Back() => voreMenu.OnEnable();
    }
}