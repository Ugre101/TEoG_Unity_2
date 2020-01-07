using TMPro;
using UnityEngine;

public class QuestMenuHandler : MonoBehaviour
{
    [Header("Quest prefab")]
    public QuestMiniBtn Prefab = null;

    [SerializeField]
    private PlayerMain player = null;

    [SerializeField]
    private TextMeshProUGUI bigQuestText;

    [SerializeField]
    private Transform miniQuestContainer = null;

    //  private Quest last;
    private void OnEnable()
    {
        miniQuestContainer.KillChildren();

        foreach (BasicQuest q in QuestsSystem.List)
        {
            QuestPrefab(q);
        }
    }

    private void QuestPrefab(BasicQuest q)
    {
        QuestMiniBtn miniQuest = Instantiate(Prefab, miniQuestContainer);
        miniQuest.Init(q, bigQuestText);
        /*   TextMeshProUGUI[] texts = AQuest.GetComponentsInChildren<TextMeshProUGUI>();
           TextMeshProUGUI title = texts[0];
           TextMeshProUGUI info = texts[1];
           title.text = $"{q.Title}";
           info.text = $"Completed: {q.Completed}{nl}Count: {q.Count}{nl}";
           if (q.HasTiers)
           {
            //   q.HasTiers ? $"Tier: {q.Tier}" : "";
           }
           Image icon = AQuest.gameObject.transform.GetChild(3).GetComponent<Image>();
           icon.sprite = null; */
    }
}