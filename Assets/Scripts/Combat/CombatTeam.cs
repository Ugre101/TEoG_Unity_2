using System.Collections.Generic;
using UnityEngine;

public class CombatTeam : MonoBehaviour
{
    public GameUI gameUI;
    public List<BasicChar> Team;
    public GameObject TeamContainer;
    public GameObject StatusPrefab;
    private List<CombatStatus> combatStatuses => new List<CombatStatus>(TeamContainer.GetComponentsInChildren<CombatStatus>());

    // if nobody is alive return true else false
    public bool TeamDead => !combatStatuses.Exists(s => s.Dead == false);

    private void Start()
    {
    }

    public void StartFight(List<BasicChar> EnemyTeam)
    {
        Team = EnemyTeam;
        foreach (Transform child in TeamContainer.transform)
        {
            Destroy(child.gameObject);
        }
        if (Team.Count < 1) // if team is less than 1 an error must have occured
        {
            gameUI.Resume();
        }
        else
        {
            foreach (BasicChar combatant in Team)
            {
                GameObject StatusToAdd = StatusPrefab;
                CombatStatus status = StatusToAdd.GetComponent<CombatStatus>();
                status.Setup(combatant, this);
                Instantiate(StatusToAdd, TeamContainer.transform);
            }
        }
    }

    public delegate void TeamLost();

    public static event TeamLost Lost;

    public void WeLost()
    {
        if (TeamDead)
        {
            Lost?.Invoke();
        }
    }
}