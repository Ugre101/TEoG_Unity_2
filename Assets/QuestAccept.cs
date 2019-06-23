using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestAccept : MonoBehaviour
{
    public playerMain player;
    public TextMeshProUGUI textBox;
    protected Quests type;

   
    public virtual void Accept()
    {
        player.Quest.AddQuest(type);
    }
}
