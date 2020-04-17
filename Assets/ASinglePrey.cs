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
        private string PreyFName => prey.Prey.Identity.FirstName;
        private Body PreyBody => prey.Prey.Body;
        private bool DigestActive => VoreOrgan.Digestion;
        private float DigestionProgress => prey.Progress;

        private void Start()
        {
            player = player != null ? player : PlayerMain.GetPlayer;
            backBtn.onClick.AddListener(Back);
            reguBtn.onClick.AddListener(Regurgileta);
        }

        public void Setup(ThePrey prey, VoreContainers voreContainer)
        {
            gameObject.SetActive(true);
            this.prey = prey;
            this.voreContainer = voreContainer;
            title.text = prey.Prey.Identity.FullName;
        }

        private void Regurgileta()
        {
            if (DigestionProgress < 0.5f)
            {
                Vector3 middleOfMap = MapEvents.CurrentMap.cellBounds.center;

                player.VoreChar.GetVoreContainer(voreContainer).ReleasePreyTo(prey, enemyContainer, player.transform.position);
            }
            else
            {
                // Dead
            }
        }

        private void PreyDesc()
        {
            string desc = string.Empty;
            if (DigestionProgress > 0)
            {
                if (DigestionProgress < 0.3f)
                {
                    desc += $"{PreyFName} has just started to digest"; //Starting to digest
                }
                else if (DigestionProgress < 0.5f)
                {
                    desc += $""; // Almost half digested check amount fat & muscle
                }
                else if (DigestionProgress < 0.8f)
                {
                    desc += $""; // More than half way digested
                }
                else
                {
                    desc += $""; // Almost fully digested
                }
            }
        }

        private void Back() => voreMenu.OnEnable();
    }
}