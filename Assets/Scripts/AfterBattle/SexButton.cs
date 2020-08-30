using System;

public class SexButton : AfterBattleButtonBase
{
    public SexScenes Scene { get; private set; }

    protected override void Func() => PlayScene?.Invoke(Scene);

    public void Setup(SexScenes parScene)
    {
        BaseSetup();
        Scene = parScene;
        title.text = Scene.name;
        btn.onClick.AddListener(Func);
    }

    public static Action<SexScenes> PlayScene;
}