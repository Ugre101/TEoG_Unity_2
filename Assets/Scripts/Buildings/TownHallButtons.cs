using UnityEngine;

public class TownHallButtons : MonoBehaviour
{
    public GameObject buyHouse, banditQuest, elfQuest;
    public playerMain player;

    private void OnEnable()
    {
        banditQuest.SetActive(player.Quest.List.Exists(q => q.Type == Quests.Bandit));
        elfQuest.SetActive(player.Quest.List.Exists(q => q.Type == Quests.Elfs));
        buyHouse.SetActive(player.PlayerFlags.BeatenBanditLord ? true : false);// replace true with house owned
    }
}