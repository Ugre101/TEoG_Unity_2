using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestAccept : MonoBehaviour
{
    public Quests type;

    [SerializeField]
    private PlayerMain player = null;

    [SerializeField]
    private Button accept = null;

    [SerializeField]
    private TextMeshProUGUI textBox = null;

    private void Start() => accept.onClick.AddListener(Accept);

    private void OnDisable() => textBox.text = "";

    public void Setup(Quests toAdd)
    {
        type = toAdd;
        textBox.text = new QuestDesc(type).Desc;
    }

    public void Accept() => player.Quest.AddQuest(type);
}