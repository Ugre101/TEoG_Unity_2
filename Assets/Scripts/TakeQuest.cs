using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TakeQuest : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title = null, textBox = null;

    [SerializeField]
    private Button accept = null, decline = null;

    private Button callBtn = null;

    private Quests quest;

    private PlayerMain player;

    private void Start()
    {
        decline.onClick.AddListener(DeclineQuest);
        accept.onClick.AddListener(AcceptQuest);
    }

    public void Setup(Quests whichQuest, PlayerMain parPlayer, Button btn)
    {
        gameObject.SetActive(true);
        GameManager.KeyBindsActive = false;
        player = parPlayer;
        quest = whichQuest;
        callBtn = btn;
        title.text = whichQuest.ToString();
        textBox.text = QuestDesc.GetDesc(whichQuest);
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