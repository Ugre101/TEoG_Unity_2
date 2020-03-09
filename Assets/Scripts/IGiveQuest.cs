using System.Collections.Generic;

public interface IGiveQuest
{
    List<QuestButton> QuestToGive { get; }
    TakeQuest QuestPanelPrefab { get; }
}