using UnityEngine;

public class BuildingsCanvas : MonoBehaviour
{
    [SerializeField] private GameObject Buildings = null;

    // Start is called before the first frame update
    private void Start()
    {
        EnterBuilding.Enter += EnterABuilding;
        CanTelePortTo.WalkedOnTeleport += TeleportMenu;
        GameManager.GameStateChangeEvent += LeaveBuilding;
    }

    private void LeaveBuilding(GameState state)
    {
        if (state == GameState.InBuilding)
        {
            Buildings.SetActive(true);
        }
        else if (state == GameState.PauseMenu)
        {
        }
        else
        {
            Buildings.transform.SleepChildren();
            Buildings.SetActive(false);
        }
    }

    public void EnterABuilding(GameObject building)
    {
        GameManager.SetCurState(GameState.InBuilding);
        Buildings.SetActive(true);
        Buildings.transform.SleepChildren(building.transform);
    }

    public void EnterABuilding(Building building) => EnterABuilding(building.gameObject);

    [SerializeField] private GameObject teleportMenu = null;

    public void TeleportMenu() => EnterABuilding(teleportMenu);
}