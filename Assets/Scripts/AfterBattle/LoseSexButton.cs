using System;

public class LoseSexButton : AfterBattleButtonBase
{
    private SexScenes scene;

    private void Func() => PlayScene?.Invoke(scene);

    public void Setup(PlayerMain playerMain, BasicChar basicChar, LoseMain loseMain, LoseScene loseScene)
    {
        this.scene = loseScene;
        title.text = scene.name;
        btn.onClick.AddListener(Func);
    }
    public static Action<SexScenes> PlayScene;
}