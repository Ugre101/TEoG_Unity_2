using System;

public class EssSexButton : AfterBattleButtonBase
{
    public EssScene Scene { get; private set; }

    public void Setup(EssScene parScene)
    {
        Scene = parScene;
        title.text = parScene.name;
        btn.onClick.AddListener(Func);
    }

    private void Func() => PlayScene?.Invoke(Scene);

    public static Action<EssScene> PlayScene;
}