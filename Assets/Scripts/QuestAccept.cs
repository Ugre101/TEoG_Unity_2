using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestAccept : MonoBehaviour
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
}
