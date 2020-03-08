using System;

public class VoreButton : AfterBattleButtonBase
{
    public VoreScene voreScene;

    private void Vore() => PlayerScene?.Invoke(voreScene);

    public void Setup(VoreScene parScene)
    {
        voreScene = parScene;
        title.text = voreScene.name;
        btn.onClick.AddListener(Vore);
    }

    public static Action<VoreScene> PlayerScene;
}