using EssenceMenuStuff;
using UnityEngine;
using Vore;

[System.Serializable]
public class MenuPanels
{
    [SerializeField] private SaveSrollListControl savemenu = null;
    [SerializeField] private OptionButtons options = null;
    [SerializeField] private EventLogHandlerBase bigeventlog = null;
    [SerializeField] private QuestMenuHandler questMenu = null;
    [SerializeField] private InventoryHandler inventory = null;
    [SerializeField] private VoreMenuHandler vore = null;
    [SerializeField] private EssenceMenu essence = null;
    [SerializeField] private PerkTreeController levelUp = null;
    [SerializeField] private LooksMenu looks = null;

    public GameObject Savemenu => savemenu.gameObject;
    public GameObject Options => options.gameObject;
    public GameObject Bigeventlog => bigeventlog.gameObject;
    public GameObject QuestMenu => questMenu.gameObject;
    public GameObject Inventory => inventory.gameObject;
    public GameObject Vore => vore.gameObject;
    public GameObject Essence => essence.gameObject;
    public GameObject LevelUp => levelUp.gameObject;
    public GameObject Looks => looks.gameObject;
}