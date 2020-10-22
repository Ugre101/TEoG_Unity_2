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
        AfterBattleMain.HideButtons += HideBtn;
        AfterBattleMain.RefreshScenes += CanDo;
    }

    public static Action<VoreScene> PlayerScene;
    public void CanDo(BasicChar caster, BasicChar target) => gameObject.SetActive(caster.Vore.Active && voreScene.CanDo(caster, target));
}