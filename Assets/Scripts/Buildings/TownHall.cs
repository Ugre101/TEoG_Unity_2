using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownHall : Building
{
    [SerializeField] private TextMeshProUGUI changeNameText = null;

    [SerializeField] private GameObject nameBox = null;

    [SerializeField] private TextMeshProUGUI textBox = null;
    [SerializeField] private GiveQuests giveQuests = new GiveQuests();
    [SerializeField] private GiveQuestRewards questRewards = new GiveQuestRewards();
    [SerializeField] private Button btn = null;
    private string SetChangeNameBtnText { set => changeNameText.text = value; }
    private string SetTextBox { set { textBox.text = value; } }

    // Start is called before the first frame update
    public void Start()
    {
        giveQuests.QuestButtons.ForEach(q => q.Btn.onClick.AddListener(()
            => Instantiate(giveQuests.QuestPanelPrefab, transform).Setup(q)));
        questRewards.RewardButtons.ForEach(q => q.Btn.onClick.AddListener(()
            =>
        { textBox.text = QuestReward.GetReward(q.Quest); q.Btn.gameObject.SetActive(false); }));
    }

    // TODO quest reward
    public override void OnEnable()
    {
        base.OnEnable();
        textBox.text = "Welcome";
        giveQuests.AlreadyHasQuest();
        questRewards.RewardButtons.ForEach(q => q.Btn.gameObject.SetActive(QuestsSystem.QuestIsCompleted(q.Quest)));
    }

    public void ToggleNameChange()
    {
        bool isActive = nameBox.activeSelf;
        nameBox.SetActive(!isActive);
        SetChangeNameBtnText = isActive ? "Change name" : "Back";
        SetTextBox = "";
    }
}