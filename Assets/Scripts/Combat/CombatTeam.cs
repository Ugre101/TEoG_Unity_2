using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTeam : MonoBehaviour
{
    public GameUI gameUI;
    public List<ThePrey> Team;
    public GameObject TeamContainer;
    public CombatStatus CombatStatusPrefab;
    public CombatMain combatMain;
    private List<CombatStatus> combatStatuses = new List<CombatStatus>();

    // if nobody is alive return true else false
    public bool TeamDead => !combatStatuses.Exists(s => s.Dead == false);

    public IEnumerator StartFight(List<ThePrey> EnemyTeam)
    {
        transform.KillChildren(TeamContainer.transform);
        Team = EnemyTeam;
        // Wait one frame so all children are properly dead...
        yield return null;
        if (Team.Count < 1) // if team is less than 1 an error must have occured
        {
            gameUI.Resume();
        }
        else
        {
            foreach (ThePrey combatant in Team)
            {
                CombatStatus StatusToAdd = Instantiate(CombatStatusPrefab, TeamContainer.transform);
                StatusToAdd.Setup(combatant, this, combatMain);
            }
            combatStatuses = new List<CombatStatus>(TeamContainer.GetComponentsInChildren<CombatStatus>());
        }
    }

    public void WeLost()
    {
        if (TeamDead)
        {
            combatMain.SomeOneDead();
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