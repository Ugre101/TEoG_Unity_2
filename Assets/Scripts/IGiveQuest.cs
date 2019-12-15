using System.Collections.Generic;
public interface IGiveQuest
{
    bool PlayerHasQuest(List<Quest> playerQuestList);
    void GiveQuest(List<Quest> playerQuestList);
}