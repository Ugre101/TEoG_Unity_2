using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vore
{
    public class VoreMenuHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMain player = null;

        [SerializeField] private TextMeshProUGUI organText = null;

        [SerializeField] private DisplayVorePrey vorePrey = null;

        [SerializeField] private Transform preyContainer = null;

        [SerializeField] private Button digestionBtn = null;

        [SerializeField] private TextMeshProUGUI digestionBtnText = null;

        [SerializeField]
        private Button sortAll = null, sortStomach = null, sortAnal = null
            , sortBalls = null, sortBoobs = null, sortVagina = null;

        private void Start()
        {
            player = player != null ? player : PlayerMain.GetPlayer;
            VoreEngine vore = player.Vore;
            sortAll.onClick.AddListener(ShowAll);
            sortStomach.onClick.AddListener(() => SortPrey(vore.Stomach));
            sortAnal.onClick.AddListener(() => SortPrey(vore.Anal));
            sortBalls.onClick.AddListener(() => SortPrey(vore.Balls));
            sortBoobs.onClick.AddListener(() => SortPrey(vore.Boobs));
            sortVagina.onClick.AddListener(() => SortPrey(vore.Vagina));
            sortAll.onClick.Invoke();
        }

        private void OnEnable() => sortAll.onClick.Invoke();

        private void ShowAll()
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
            organText.text = "All";
            digestionBtn.onClick.RemoveAllListeners();
            digestionBtn.gameObject.SetActive(false);
        }

        private void SortPrey(VoreBasic voreOrgan)
        {
            preyContainer.KillChildren();
            organText.text = voreOrgan.VoreContainers.ToString();
            digestionBtn.gameObject.SetActive(true);
            digestionBtn.onClick.RemoveAllListeners();
            digestionBtn.onClick.AddListener(() => { digestionBtnText.text = $"Digestion:\n{voreOrgan.ToggleDigestion}"; });
            digestionBtnText.text = $"Digestion:\n{voreOrgan.Digestion}";
            SetupPrey(voreOrgan);
        }

        private void SetupPrey(VoreBasic voreOrgan) => voreOrgan.Preys.ForEach(p =>
        Instantiate(vorePrey, preyContainer).Setup(p, voreOrgan.VoreContainers).onClick.AddListener(() => ClickPrey(p)));

        private void ClickPrey(ThePrey prey)
        {
            Debug.Log(prey.Prey.Identity.FullName);
            // TODO something
        }
    }
}