using TMPro;
using UnityEngine;

[System.Serializable]
public abstract class NpcMenuPage : MonoBehaviour
{
    [SerializeField] protected Npc theNpc = null;
    [SerializeField] protected TextMeshProUGUI textLog = null, title = null;
    [SerializeField] protected NpcInfo npcInfo = null;
    protected abstract string TitleName { get; }

    protected virtual void Start()
    {
        npcInfo.Setup(theNpc);
        title.text = TitleName;
    }
}

public class FarmCentaurNpcMenu : NpcMenuPage
{
    protected override string TitleName => "Centaur";
}