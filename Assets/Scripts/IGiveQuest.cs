using System.Collections.Generic;
using UnityEngine.UI;

public interface IGiveQuest
{
    List<QuestButton> QuestToGive { get; }
    TakeQuest QuestPanelPrefab { get; }
}

[System.Serializable]
public class QuestButton
{
    [field: UnityEngine.SerializeField] public Button Btn { get; private set; }
    [field: UnityEngine.SerializeField] public Quests Quest { get; private set; }
}