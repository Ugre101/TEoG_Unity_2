using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Vore
{
    public class VoreMenuHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMain player = null;

        [SerializeField] private TextMeshProUGUI organText = null, capacityText = null;

        [SerializeField] private DisplayVorePrey vorePrey = null;

        [SerializeField] private Transform preyContainer = null, optionContainer = null, allPreys = null;

        [SerializeField] private VoreOptionBtnDigestion optionBtn = null;
        [SerializeField] private VoreOptionBtnRebirth rebithBtn = null;
        [SerializeField] private VoreOptionBtnDrainEss drainEssBtn = null;

        [SerializeField]
        private Button sortAll = null, sortStomach = null, sortAnal = null
            , sortBalls = null, sortBoobs = null, sortVagina = null;

        [SerializeField] private ASinglePrey singlePrey = null;

        private void Start()
        {
            player = player ?? PlayerHolder.Player;
            VoreEngine vore = player.Vore;
            sortAll.onClick.AddListener(ShowAll);
            sortStomach.onClick.AddListener(() => SortPrey(vore.Stomach));
            sortAnal.onClick.AddListener(() => SortPrey(vore.Anal));
            sortBalls.onClick.AddListener(() => SortPrey(vore.Balls));
            sortBoobs.onClick.AddListener(() => SortPrey(vore.Boobs));
            sortVagina.onClick.AddListener(() => SortPrey(vore.Vagina));
            sortAll.onClick.Invoke();
        }

        public void OnEnable()
        {
            sortAll.onClick.Invoke();
            allPreys.gameObject.SetActive(true);
            singlePrey.gameObject.SetActive(false);
        }

        private void Update() => ShowCapacityAll();

        private void ShowAll()
        {
            optionContainer.KillChildren();
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
            ChangeDrainEss();
        }

        private void SortPrey(VoreBasic voreOrgan)
        {
            preyContainer.KillChildren();
            optionContainer.KillChildren();
            Instantiate(optionBtn, optionContainer).Setup(voreOrgan);
            organText.text = voreOrgan.VoreContainers.ToString();
            SetupPrey(voreOrgan);
            if (voreOrgan is VoreVagina voreVagina && player.Vore.Perks.HasPerk(VorePerks.ReBirth))
            {
                Instantiate(rebithBtn, optionContainer).Setup(voreVagina);
            }
            ChangeDrainEss();
        }

        private void SetupPrey(VoreBasic voreOrgan) => voreOrgan.Preys.ForEach(p =>
        Instantiate(vorePrey, preyContainer).Setup(p, voreOrgan.VoreContainers).onClick.AddListener(() => ClickPrey(p, voreOrgan.VoreContainers)));

        private void ClickPrey(ThePrey prey, VoreContainers voreContainers)
        {
            allPreys.gameObject.SetActive(false);
            singlePrey.Setup(prey, voreContainers);
        }

        private void ShowCapacityAll()
        {
            VoreEngine vore = player.Vore;
            string capaText = Capacity(vore.Stomach);
            HasAdd(vore.Balls.MaxCapacity() > 0, Capacity(vore.Balls));
            HasAdd(vore.Boobs.MaxCapacity() > 0, Capacity(vore.Boobs));
            HasAdd(vore.Vagina.MaxCapacity() > 0, Capacity(vore.Vagina));
            capaText += $"\n{Capacity(vore.Anal)}";
            capacityText.text = capaText;
            void HasAdd(bool has, string add)
            {
                if (has)
                {
                    capaText += $"\n{add}";
                }
            }
        }

        private void ChangeDrainEss()
        {
            if (player.Vore.Perks.HasPerk(VorePerks.DrainEssence))
            {
                Instantiate(drainEssBtn, optionContainer).Setup();
            }
        }

        private string Capacity(VoreBasic organ) => $"{organ.VoreContainers.ToString()}: {organ.Current.KgorPWithOutSuffix()}/{organ.MaxCapacity().KgorP()}";
    }
}