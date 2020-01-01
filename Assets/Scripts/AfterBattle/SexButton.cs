using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SexButton : MonoBehaviour
{
    [SerializeField]
    private AfterBattleMain afterBattle = null;

    [SerializeField]
    private SexScenes scene = null;

    [SerializeField]
    private Button btn = null;

    [SerializeField]
    private TextMeshProUGUI title = null;

    private PlayerMain player;
    private BasicChar other;

    public void Start()
    {
        if (btn == null)
        {
            btn = GetComponent<Button>();
        }
    }

    private void Func()
    {
        afterBattle.AddToTextBox(afterBattle.LastScene == scene ? scene.ContinueScene(player, other) : scene.StartScene(player, other));
        afterBattle.LastScene = scene;
    }

    public void Setup(PlayerMain parPlayer, BasicChar parPartner, AfterBattleMain parAfterBattle, SexScenes parScene)
    {
        player = parPlayer;
        other = parPartner;
        afterBattle = parAfterBattle;
        scene = parScene;
        title.text = scene.name;
        btn.onClick.AddListener(Func);
    }
}