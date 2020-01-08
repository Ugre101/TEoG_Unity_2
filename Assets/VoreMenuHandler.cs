using UnityEngine;
using UnityEngine.UI;

namespace Vore
{
    public class VoreMenuHandler : MonoBehaviour
    {
        [SerializeField]
        private PlayerMain player = null;

        [SerializeField]
        private DisplayVorePrey vorePrey = null;

        [SerializeField]
        private Transform preyContainer = null;

        [SerializeField]
        private Button sortAll = null, sortStomach = null, sortAnal = null
            , sortBalls = null, sortBoobs = null, sortVagina = null;

        private void Start()
        {
            player = player != null ? player : PlayerMain.GetPlayer;
            VoreEngine vore = player.Vore;
            sortAll.onClick.AddListener(OnEnable);
            sortStomach.onClick.AddListener(() => SortPrey(vore.Stomach));
            sortAnal.onClick.AddListener(() => SortPrey(vore.Anal));
            sortBalls.onClick.AddListener(() => SortPrey(vore.Balls));
            sortBoobs.onClick.AddListener(() => SortPrey(vore.Boobs));
            sortVagina.onClick.AddListener(() => SortPrey(vore.Vagina));
        }

        private void OnEnable()
        {
            preyContainer.KillChildren();
            if (player != null)
            {
                VoreEngine vore = player.Vore;
                if (vore.Active)
                {
                    SetupPrey(vore.Stomach);
                    SetupPrey(vore.Anal);
                    SetupPrey(vore.Balls);
                    SetupPrey(vore.Boobs);
                    SetupPrey(vore.Vagina);
                }
            }
        }

        private void SortPrey(VoreBasic voreOrgan)
        {
            preyContainer.KillChildren();
            SetupPrey(voreOrgan);
        }

        private void SetupPrey(VoreBasic voreOrgan)
        {
            voreOrgan.Preys.ForEach(p =>
            {
                DisplayVorePrey dv = Instantiate(vorePrey, preyContainer);
                dv.Setup(p, voreOrgan.VoreContainers);
            });
        }
    }
}