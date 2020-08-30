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
        foreach (BasicQuest basicQuest in QuestsSystem.BasicQuests.Values)
        {
            QuestPrefab(basicQuest);
        }
        foreach (CountQuest countQuest in QuestsSystem.CountQuests.Values)
        {
            QuestPrefab(countQuest);
        }
        foreach (TieredQuest tieredQuest in QuestsSystem.TieredQuests.Values)
        {
            QuestPrefab(tieredQuest);
        }
    }

    private void QuestPrefab(BasicQuest q) => Instantiate(Prefab, miniQuestContainer).Init(q, bigQuest);
}