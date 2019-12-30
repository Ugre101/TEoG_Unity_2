using UnityEngine;

public class TownHallButtons : MonoBehaviour
{
    public GameObject buyHouse, banditQuest, elfQuest;
    public PlayerMain player;

    private void OnEnable()
    {
        banditQuest.SetActive(QuestsSystem.List.Exists(q => q.Type == Quests.Bandit));
        elfQuest.SetActive(QuestsSystem.List.Exists(q => q.Type == Quests.Elfs));
        buyHouse.SetActive(player.PlayerFlags.BeatBanditLord.Cleared);
    }
}