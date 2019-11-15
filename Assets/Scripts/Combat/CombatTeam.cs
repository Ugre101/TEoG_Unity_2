using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTeam : MonoBehaviour
{
    public GameUI gameUI;
    public List<BasicChar> Team;
    public GameObject TeamContainer;
    public CombatStatus CombatStatusPrefab;
    public CombatMain combatMain;
    private List<CombatStatus> combatStatuses = new List<CombatStatus>();

    // if nobody is alive return true else false
    public bool TeamDead => !combatStatuses.Exists(s => s.Dead == false);
    private void OnEnable()
    {

    }
   
    public IEnumerator StartFight(List<BasicChar> EnemyTeam)
    {
        foreach (Transform child in TeamContainer.transform)
        {
            Destroy(child.gameObject);
        }
        Team = EnemyTeam;
        // Wait one frame so all children are properly dead...
        yield return null;
        if (Team.Count < 1) // if team is less than 1 an error must have occured
        {
            gameUI.Resume();
        }
        else
        {
            foreach (BasicChar combatant in Team)
            {
                CombatStatus StatusToAdd = Instantiate(CombatStatusPrefab, TeamContainer.transform);
                StatusToAdd.Setup(combatant, this, combatMain);
            }
            combatStatuses = new List<CombatStatus>(TeamContainer.GetComponentsInChildren<CombatStatus>());
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

    public void DeSelectAll()
    {
        foreach (CombatStatus cs in combatStatuses)
        {
            cs.DeSelect();
        }
    }
}