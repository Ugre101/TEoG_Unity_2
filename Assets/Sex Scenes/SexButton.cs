using UnityEngine;
using UnityEngine.UI;
public class SexButton : MonoBehaviour
{
    public AfterBattleActions afterbattle;
    public SexScenes scene;
    public Button btn;

    private playerMain player;
    private BasicChar other;
    public void Start()
    {
        btn = GetComponent<Button>();
        afterbattle = GetComponentInParent<AfterBattleActions>();
        if (afterbattle != null)
        {
            player = afterbattle.player;
            other = afterbattle.enemies[0];
        }
        btn.onClick.AddListener(Func);
    }
    private void Func()
    {
        afterbattle.AddToTextBox(afterbattle.LastScene == scene ? scene.ContinueScene(player,other) : scene.StartScene(player,other));
        afterbattle.LastScene = scene;
    }
}
