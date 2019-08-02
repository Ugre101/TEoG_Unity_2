using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeam : MonoBehaviour
{
    public List<EnemyPrefab> Team;
    public GameObject TeamContainer;
    public GameObject StatusPrefab;

    public void StartFight(List<EnemyPrefab> EnemyTeam)
    {
        Team = EnemyTeam;
        foreach (Transform child in TeamContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (BasicChar combatant in Team)
        {
            GameObject StatusToAdd = StatusPrefab;
            CombatStatus status = StatusToAdd.GetComponent<CombatStatus>();
            status.Setup(combatant);
            Instantiate(StatusToAdd, TeamContainer.transform);
        }
    }
}
