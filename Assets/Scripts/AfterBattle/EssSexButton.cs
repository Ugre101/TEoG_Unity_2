using System;

public class EssSexButton : AfterBattleButtonBase
{
    private EssScene scene;

    public void Setup(EssScene parScene)
    {
        scene = parScene;
        title.text = parScene.name;
        btn.onClick.AddListener(Func);
    }

    private void Func() => PlayScene?.Invoke(scene);

    public static Action<EssScene> PlayScene;
}