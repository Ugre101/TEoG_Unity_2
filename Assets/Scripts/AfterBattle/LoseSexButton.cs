using System;

public class LoseSexButton : AfterBattleButtonBase
{
    private SexScenes scene;

    protected override void Func() => PlayScene?.Invoke(scene);

    public void Setup(PlayerMain playerMain, BasicChar basicChar, LoseMain loseMain, LoseScene loseScene)
    {
        BaseSetup();
        this.scene = loseScene;
        title.text = scene.name;
        btn.onClick.AddListener(Func);
    }

    public static Action<SexScenes> PlayScene;
}