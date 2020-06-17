using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private BigPanel Gameui = null;

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.GameStateChangeEvent += ShowGameUI;
    }

    // Update is called once per frame
    private void Update()
    {
        if (KeyBindings.HideAllKey.KeyDown)
        {
            if (GameManager.CurState.Equals(GameState.Free))
            {
                Gameui.gameObject.SetActive(!Gameui.gameObject.activeSelf);
                gameUI_Active = Gameui.gameObject.activeSelf;
            }
        }
    }

    private bool gameUI_Active = true;

    /// <summary> Hides gameUI and returns if gameUi was active before </summary>
    /// <returns></returns>
    public bool HideGameUI()
    {
        bool currState = Gameui.gameObject.activeSelf;
        Gameui.gameObject.SetActive(false);
        return currState;
    }

    public void ShowGameUI(GameState state)
    {
        if (state == GameState.Free)
        {
            Gameui.gameObject.SetActive(gameUI_Active);
        }
        else
        {
            Gameui.gameObject.SetActive(false);
        }
    }
}