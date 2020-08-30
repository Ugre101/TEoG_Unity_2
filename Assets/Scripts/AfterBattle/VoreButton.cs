using System;

public class VoreButton : AfterBattleButtonBase
{
    public VoreScene voreScene;

    protected override void Func() => PlayerScene?.Invoke(voreScene);

    public void Setup(VoreScene parScene)
    {
        BaseSetup();
        voreScene = parScene;
        title.text = voreScene.name;
        btn.onClick.AddListener(Func);
    }

    public static Action<VoreScene> PlayerScene;
}