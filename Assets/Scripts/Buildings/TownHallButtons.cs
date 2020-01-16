using UnityEngine;

public class TownHallButtons : MonoBehaviour
{
    public GameObject buyHouse, banditQuest, elfQuest;
    public PlayerMain player;

    private void OnEnable()
    {
        banditQuest.SetActive(QuestsSystem.HasQuest(Quests.Bandit));
        elfQuest.SetActive(QuestsSystem.HasQuest(Quests.ElfsHunt));
        buyHouse.SetActive(PlayerFlags.BeatBanditLord.Cleared);
    }
}