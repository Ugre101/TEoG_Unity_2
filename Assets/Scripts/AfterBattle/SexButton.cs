using UnityEngine;

public class SexButton : SexButtonBase
{
    [SerializeField]
    private AfterBattleMain afterBattle = null;

    private void Func()
    {
        afterBattle.AddToTextBox(afterBattle.LastScene == scene ? scene.ContinueScene(player, other) : scene.StartScene(player, other));
        afterBattle.LastScene = scene;
        scene.ArousalGain(player, other);
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