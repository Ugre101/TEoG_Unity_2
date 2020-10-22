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
        AfterBattleMain.HideButtons += HideBtn;
        AfterBattleMain.RefreshScenes += CanDo;
    }

    public static Action<SexScenes> PlayScene;
    public void CanDo(BasicChar caster, BasicChar target) => gameObject.SetActive(caster.CanOrgasmMore() && Scene.CanDo(caster, target));
}