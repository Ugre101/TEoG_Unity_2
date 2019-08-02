using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public List<BasicChar> Team;
    public GameObject TeamContainer;
    public GameObject StatusPrefab;

    void OnEnable()
    {
        StartFight();
    }
    public void StartFight()
    {
        foreach(Transform child in TeamContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(BasicChar combatant in Team)
        {
            GameObject StatusToAdd = StatusPrefab;
            CombatStatus status = StatusToAdd.GetComponent<CombatStatus>();
            status.Setup(combatant);
            Instantiate(StatusToAdd, TeamContainer.transform);
        }
    }
}
