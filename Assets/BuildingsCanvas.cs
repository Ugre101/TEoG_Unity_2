using UnityEngine;

public class BuildingsCanvas : MonoBehaviour
{
    [SerializeField] private BigPanel Buildings = null;
    [SerializeField] private BuildingsMenu buildings = null;

    // Start is called before the first frame update
    private void Start()
    {
        EnterBuilding.Enter += EnterABuilding;
        CanTelePortTo.WalkedOnTeleport += TeleportMenu;
        GameManager.GameStateChangeEvent += LeaveBuilding;
    }

    private void LeaveBuilding(GameState state)
    {
        if (state == GameState.InBuilding || state == GameState.PauseMenu)
        {
        }
        else
        {
            Buildings.transform.SleepChildren();
            Buildings.gameObject.SetActive(false);
        }
    }

    public void EnterABuilding(Building building)
    {
        GameManager.SetCurState(GameState.InBuilding);
        Buildings.gameObject.SetActive(true);
        Buildings.transform.SleepChildren(building.transform);
    }

    [SerializeField] private GameObject teleportMenu = null;

    public void TeleportMenu()
    {
        GameManager.SetCurState(GameState.InBuilding);
        Buildings.gameObject.SetActive(true);
        Buildings.transform.SleepChildren(teleportMenu.transform);
    }
}