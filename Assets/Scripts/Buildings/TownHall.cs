using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownHall : Building
{
    [SerializeField]
    private TextMeshProUGUI changeNameText;

    [SerializeField]
    private GameObject nameBox;

    [SerializeField]
    private TextMeshProUGUI textBox;

    private string SetChangeNameBtnText { set => changeNameText.text = value; }
    private string SetTextBox { set { textBox.text = value; } }

    [field: SerializeField] public List<QuestButton> QuestToGive { get; private set; }

    [field: SerializeField] public TakeQuest QuestPanelPrefab { get; private set; }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        QuestToGive.ForEach(q => q.Btn.onClick.AddListener(() =>
        {
            TakeQuest temp = Instantiate(QuestPanelPrefab, transform);
            temp.Setup(q.Quest, player, q.Btn);
        }));
    }

    private void OnEnable()
    {
        foreach (QuestButton qb in QuestToGive)
        {
            if (QuestsSystem.HasQuest(qb.Quest))
            {
                qb.Btn.gameObject.SetActive(false);
                // is quest finished?
            }
            else
            {
                qb.Btn.gameObject.SetActive(true);
            }
        }
    }

    public void ToggleNameChange()
    {
        bool isActive = nameBox.activeSelf;
        nameBox.SetActive(!isActive);
        SetChangeNameBtnText = isActive ? "Change name" : "Back";
        SetTextBox = "";
    }

}