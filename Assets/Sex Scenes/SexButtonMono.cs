using UnityEngine;
using UnityEngine.UI;
public class SexButtonMono : MonoBehaviour
{
    public AfterBattleActions afterBattle;
    public MonoScene monoScene;
    public Button btn;

    private playerMain player;
    private BasicChar other;
    void Start()
    {
        btn = GetComponent<Button>();
        afterBattle = GetComponentInParent<AfterBattleActions>();
        if (afterBattle != null)
        {
            player = afterBattle._player;
            other = afterBattle.enemies[0];
        }
        btn.onClick.AddListener(Func);
    }
    private void Func()
    {
        monoScene.DoScene(player, other);
    }
}
