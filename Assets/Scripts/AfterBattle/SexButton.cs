using System;

public class SexButton : AfterBattleButtonBase
{
    private SexScenes scene;

    private void Func() => PlayScene?.Invoke(scene);

    public void Setup(SexScenes parScene)
    {
        scene = parScene;
        title.text = scene.name;
        btn.onClick.AddListener(Func);
    }

    public static Action<SexScenes> PlayScene;
}