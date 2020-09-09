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
        switch (state)
        {
            case GameState.InBuilding:
                Buildings.SetActive(true);
                break;
            case GameState.PauseMenu:
                break;
            default:
                Buildings.transform.SleepChildren();
                Buildings.SetActive(false);
                break;
        }
    }

    private void EnterABuilding(GameObject building)
    {
        GameManager.SetCurState(GameState.InBuilding);
        Buildings.SetActive(true);
        Buildings.transform.SleepChildren(building.transform);
    }

    private void EnterABuilding(Building building) => EnterABuilding(building.gameObject);

    [SerializeField] private GameObject teleportMenu = null;

    private void TeleportMenu() => EnterABuilding(teleportMenu);
}