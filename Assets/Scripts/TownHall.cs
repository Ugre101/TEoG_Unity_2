using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownHall : MonoBehaviour, IGiveQuest
{
    private Quest bandit, elfHunt;
    private Button changeName;
    private GameObject nameBox;
    private TextMeshProUGUI textBox;

    public void GiveQuest(List<Quest> playerQuestList)
    {
        throw new System.NotImplementedException();
    }

    public bool PlayerHasQuest(List<Quest> playerQuestList)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ToggleNameChange()
    {
      
    }
}