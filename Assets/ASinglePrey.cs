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
            if (prey.Progress < 0.5f)
            {
                player.VoreChar.GetVoreContainer(voreContainer).ReleasePreyTo(prey, enemyContainer, player.transform.position);
            }
            else
            {
                // Dead
            }
        }

        private void Back() => voreMenu.OnEnable();
    }
}