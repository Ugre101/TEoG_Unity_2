using UnityEngine;

public class QuestMenuHandler : MonoBehaviour
{
    [Header("Quest prefab")]
    [SerializeField]
    private QuestMiniBtn Prefab = null;

    [SerializeField]
    private BigQuest bigQuest = null;

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
        miniQuest.Init(q, bigQuest);
    }
}