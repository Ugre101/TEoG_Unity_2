using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TakeQuest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title = null, textBox = null;

    [SerializeField] private Button accept = null, decline = null;

    private Button callBtn = null;

    private Quests quest;

    private void Start()
    {
        decline.onClick.AddListener(DeclineQuest);
        accept.onClick.AddListener(AcceptQuest);
    }

    public void Setup(QuestButton q)
    {
        gameObject.SetActive(true);
        GameManager.KeyBindsActive = false;
        quest = q.Quest;
        callBtn = q.Btn;
        title.text = quest.ToString();
        textBox.text = QuestDesc.GetDesc(quest);
    }

    private void DeclineQuest()
    {
        GameManager.KeyBindsActive = true;
        Destroy(gameObject);
    }

    private void AcceptQuest()
    {
        QuestsSystem.AddQuest(quest);
        callBtn.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}