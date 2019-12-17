using UnityEngine;
using UnityEngine.UI;

public class QuestAccept : MonoBehaviour
{
    public Quests type;

    [SerializeField]
    private PlayerMain player = null;

    [SerializeField]
    private Button accept = null;

    private void Start() => accept.onClick.AddListener(Accept);

    public void Setup(Quests toAdd) => type = toAdd;

    public void Accept() => player.Quest.AddQuest(type);
}