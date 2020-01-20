using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssSexButton : MonoBehaviour
{
    private AfterBattleMain afterBattle;
    private EssScene scene;

    [SerializeField] private Button btn;

    [SerializeField] private TextMeshProUGUI title = null;

    private PlayerMain Caster => afterBattle.Caster;
    private BasicChar Other => afterBattle.Target;

    public void Setup(AfterBattleMain afterBattleMain, EssScene parScene)
    {
        btn = btn != null ? btn : GetComponent<Button>();
        afterBattle = afterBattleMain;
        scene = parScene;
        title.text = parScene.name;
        btn.onClick.AddListener(Func);
    }

    private void Func()
    {
        afterBattle.AddToTextBox(afterBattle.LastScene == scene
            ? scene.ContinueScene(Caster, Other)
            : scene.StartScene(Caster, Other));
        afterBattle.LastScene = scene;
        Other.SexStats.Drained();
        afterBattle.RefreshScenes();
    }
}