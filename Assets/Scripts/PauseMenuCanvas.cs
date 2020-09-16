using UnityEngine;

public class PauseMenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu = null;

    // Start is called before the first frame update
    private void Start() => GameManager.GameStateChangeEvent += EscapePause;

    private void EscapePause(GameState state)
    {
        if (state == GameState.PauseMenu)
        {
            transform.SleepChildren(PauseMenu.transform);
        }
        else if (GameManager.LastState == GameState.PauseMenu)
        {
            transform.SleepChildren();
        }
    }
}