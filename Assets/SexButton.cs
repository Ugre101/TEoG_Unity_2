using System.Collections;
using System.Collections.Generic;
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
            player = afterbattle._player;
            other = afterbattle._enemy._enemies[0];
        }
        btn.onClick.AddListener(Func);
    }
    private void Func()
    {
        scene.DoScene(player, other);
        afterbattle.AddToTextBox(scene.Text(player,other));
    }
}
