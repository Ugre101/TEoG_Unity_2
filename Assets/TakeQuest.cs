using UnityEngine;
using UnityEngine.UI;

public class TakeQuest : MonoBehaviour
{
    [SerializeField]
    private QuestAccept questAccept = null;

    [SerializeField]
    private Button decline = null;

    private void Start() => decline.onClick.AddListener(DeclineQuest);

    public void Setup(Quests whichQuest)
    {
        gameObject.SetActive(true);
        questAccept.type = whichQuest;
        GameManager.KeyBindsActive = false;
    }

    private void DeclineQuest()
    {
        GameManager.KeyBindsActive = true;
        gameObject.SetActive(false);
    }
}