using EssenceMenuStuff;
using UnityEngine;
using Vore;

[System.Serializable]
public struct MenuPanels
{
    [SerializeField] private SaveSrollListControl savemenu;
    [SerializeField] private OptionButtons options;
    [SerializeField] private EventLogHandlerBase bigeventlog;
    [SerializeField] private QuestMenuHandler questMenu;
    [SerializeField] private InventoryHandler inventory;
    [SerializeField] private VoreMenuHandler vore;
    [SerializeField] private EssenceMenu essence;
    [SerializeField] private PerkTreeController levelUp;
    [SerializeField] private LooksMenu looks;

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