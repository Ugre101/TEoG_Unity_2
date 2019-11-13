using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SexButton : MonoBehaviour
{
    public AfterBattleMain afterBattle;
    public SexScenes scene;
    public Button btn;
    public TextMeshProUGUI title;

    private playerMain player;
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
        afterBattle.AddToTextBox(afterBattle.LastScene == scene ? scene.ContinueScene(player,other) : scene.StartScene(player,other));
        afterBattle.LastScene = scene;
    }
    public void Setup(playerMain parPlayer, BasicChar parPartner,AfterBattleMain parAfterBattle,SexScenes parScene)
    {
        player = parPlayer;
        other = parPartner;
        afterBattle = parAfterBattle;
        scene = parScene;
        title.text = scene.name;
        btn.onClick.AddListener(Func);
    }
}
