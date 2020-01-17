using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownHall : Building, IGiveQuest
{
    [SerializeField] private TextMeshProUGUI changeNameText = null;

    [SerializeField] private GameObject nameBox = null;

    [SerializeField] private TextMeshProUGUI textBox = null;

    private string SetChangeNameBtnText { set => changeNameText.text = value; }
    private string SetTextBox { set { textBox.text = value; } }

    [field: SerializeField] public List<QuestButton> QuestToGive { get; private set; }

    [field: SerializeField] public TakeQuest QuestPanelPrefab { get; private set; }

    // Start is called before the first frame update
    public void Start()
    {
        QuestToGive.ForEach(q => q.Btn.onClick.AddListener(() => Instantiate(QuestPanelPrefab, transform).Setup(q.Quest, player, q.Btn)));
    }

    public override void OnEnable()
    {
        base.OnEnable();
        QuestToGive.ForEach(qg => qg.Btn.gameObject.SetActive(QuestsSystem.HasQuest(qg.Quest)));
    }

    public void ToggleNameChange()
    {
        bool isActive = nameBox.activeSelf;
        nameBox.SetActive(!isActive);
        SetChangeNameBtnText = isActive ? "Change name" : "Back";
        SetTextBox = "";
    }
}