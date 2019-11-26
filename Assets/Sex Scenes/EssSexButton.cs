using UnityEngine;
using UnityEngine.UI;

public class EssSexButton : MonoBehaviour
{
    public AfterBattleMain afterBattle;
    public SexScenes scene;
    public Button btn;
    private playerMain Caster => afterBattle.Caster;
    private EnemyPrefab Other => afterBattle.Target;

    private void Start()
    {
        btn.onClick.AddListener(Func);
    }

    public void Func()
    {
        afterBattle.AddToTextBox(afterBattle.LastScene == scene ?
            scene.ContinueScene(Caster, Other) : scene.StartScene(Caster, Other));
        afterBattle.LastScene = scene;
        Other.SexStats.Drained();
        afterBattle.RefreshScenes();
    }
}