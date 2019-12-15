using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuestAccept : MonoBehaviour,IGiveQuest
{
    public PlayerMain player;
    public Quests type;
    public Button accept;
    public TextMeshProUGUI textBox;
   
    private void Start()
    {
        accept.onClick.AddListener(Accept);
    }

    private void OnEnable()
    {
        textBox.text = new QuestDesc(type).Desc;
    }
    private void OnDisable()
    {
        textBox.text = "";
    }
    public void Accept()
    {
        player.Quest.AddQuest(type);
    }

    public bool PlayerHasQuest(List<Quest> playerQuestList)
    {
        return playerQuestList.Exists(q => q.Type == type);
    }

    public void GiveQuest(List<Quest> playerQuestList)
    {
      
        throw new System.NotImplementedException();
    }
}
