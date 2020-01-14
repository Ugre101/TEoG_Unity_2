using UnityEngine;

public class MapQuestIcon : MonoBehaviour
{
    [SerializeField] private Quests Quest = Quests.Bandit;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private void OnEnable()
    {
        spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        QuestsSystem.GotQuestEvent += HadQuest;
        HadQuest();
    }

    private void OnDisable() => QuestsSystem.GotQuestEvent -= HadQuest;

    private void HadQuest() => spriteRenderer.enabled = QuestsSystem.HasQuest(Quest);
}