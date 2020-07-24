using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTeam : MonoBehaviour
{
    private List<BasicChar> Team = new List<BasicChar>();
    [SerializeField] private GameObject TeamContainer = null;
    [SerializeField] private CombatStatus CombatStatusPrefab = null;
    [SerializeField] private CombatMain combatMain = null;
    private List<CombatStatus> combatStatuses = new List<CombatStatus>();

    // if nobody is alive return true else false
    public bool TeamDead => !combatStatuses.Exists(s => s.Dead == false);

    public IEnumerator StartFight(List<BasicChar> EnemyTeam)
    {
        TeamContainer.transform.KillChildren();
        Team = EnemyTeam;
        // Wait one frame so all children are properly dead...
        yield return null;
        if (Team.Count < 1) // if team is less than 1 an error must have occured
        {
            GameManager.ReturnToLastState();
        }
        else
        {
            foreach (BasicChar combatant in Team)
            {
                Instantiate(CombatStatusPrefab, TeamContainer.transform).Setup(combatant, this, combatMain);
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