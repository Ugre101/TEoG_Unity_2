using UnityEngine;

public class TownHallButtons : MonoBehaviour
{
    [SerializeField] private GameObject buyHouse = null, banditQuest = null, elfQuest = null;

    private void OnEnable()
    {
        banditQuest.SetActive(QuestsSystem.HasQuest(Quests.Bandit));
        elfQuest.SetActive(QuestsSystem.HasQuest(Quests.ElfsHunt));
        buyHouse.SetActive(PlayerFlags.BeatBanditLord.Cleared);
    }
}