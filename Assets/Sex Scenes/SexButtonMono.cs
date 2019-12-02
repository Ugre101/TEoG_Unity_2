using UnityEngine;
using UnityEngine.UI;
public class SexButtonMono : MonoBehaviour
{
    public AfterBattleMain afterBattle;
    public MonoScene monoScene;
    public Button btn;

    private PlayerMain player;
    private BasicChar other;
    void Start()
    {
        btn = GetComponent<Button>();
        afterBattle = GetComponentInParent<AfterBattleMain>();
        if (afterBattle != null)
        {
            player = afterBattle.player;
            other = afterBattle.enemies[0];
        }
        btn.onClick.AddListener(Func);
    }
    private void Func()
    {
        monoScene.DoScene(player, other);
    }
}
