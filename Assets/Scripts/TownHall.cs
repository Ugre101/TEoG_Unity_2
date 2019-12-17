using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownHall : MonoBehaviour, IGiveQuest
{
    [SerializeField]
    private PlayerMain player = null;

    [SerializeField]
    private TextMeshProUGUI changeNameText;

    [SerializeField]
    private GameObject nameBox;

    [SerializeField]
    private TextMeshProUGUI textBox;

    private string SetChangeNameBtnText { set => changeNameText.text = value; }
    private string SetTextBox { set { textBox.text = value; } }

    [field: SerializeField] public List<QuestButton> QuestToGive { get; private set; }

    [field: SerializeField] public TakeQuest QuestPanel { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        QuestToGive.ForEach(q => q.Btn.onClick.AddListener(() => QuestPanel.Setup(q.Quest)));
    }

    private void OnEnable()
    {
        foreach (QuestButton qb in QuestToGive)
        {
            if (PlayerHasQuest(qb.Quest))
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

    public bool PlayerHasQuest(Quests quest) => player.Quest.HasQuest(quest);
}