using UnityEngine;

public class QuestMenuHandler : MonoBehaviour
{
    [Header("Quest prefab")]
    [SerializeField] private QuestMiniBtn Prefab = null;

    [SerializeField] private BigQuest bigQuest = null;

    [SerializeField] private Transform miniQuestContainer = null;

    //  private Quest last;
    private void OnEnable()
    {
        miniQuestContainer.KillChildren();
        QuestsSystem.BasicQuests.ForEach(bq => QuestPrefab(bq));
        QuestsSystem.CountQuests.ForEach(cq => QuestPrefab(cq));
        QuestsSystem.TieredQuests.ForEach(tq => QuestPrefab(tq));
    }

    private void QuestPrefab(BasicQuest q) => Instantiate(Prefab, miniQuestContainer).Init(q, bigQuest);
}