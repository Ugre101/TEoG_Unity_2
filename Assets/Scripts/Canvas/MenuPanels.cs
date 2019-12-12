using UnityEngine;

[System.Serializable]
public struct MenuPanels
{
    [field: SerializeField] public GameObject Savemenu { get; private set; }
    [field: SerializeField] public GameObject Options { get; private set; }
    [field: SerializeField] public GameObject Bigeventlog { get; private set; }
    [field: SerializeField] public GameObject QuestMenu { get; private set; }
    [field: SerializeField] public GameObject Inventory { get; private set; }
    [field: SerializeField] public GameObject Vore { get; private set; }
    [field: SerializeField] public GameObject Essence { get; private set; }
    [field: SerializeField] public GameObject LevelUp { get; private set; }
    [field: SerializeField] public GameObject Looks { get; private set; }
}