using System;

public class EssSexButton : AfterBattleButtonBase
{
    public EssScene Scene { get; private set; }

    public void Setup(EssScene parScene)
    {
        BaseSetup();
        Scene = parScene;
        title.text = parScene.name;
        btn.onClick.AddListener(Func);
    }

    protected override void Func() => PlayScene?.Invoke(Scene);

    public static Action<EssScene> PlayScene;
}