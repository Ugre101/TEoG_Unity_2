using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour,IGiveQuest
{ 
    private Quest bandit, elfHunt;
    
    public void GiveQuest(List<Quest> playerQuestList)
    {
        throw new System.NotImplementedException();
    }

    public bool PlayerHasQuest(List<Quest> playerQuestList)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
