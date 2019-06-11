using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AfterBattleActions : MonoBehaviour
{
    //public playerMain _player;
    public afterBattleEnemy _enemy;
    public TextMeshProUGUI _textBox;

    private void OnEnable()
    {
        if (_textBox == null)
        {
            this.enabled = false;
        }
        _textBox.text = null;
    }
    public void DrainMasc()
    {
        string drainText = "";
        drainText += _enemy.DrainMasc();
        _textBox.text = drainText;
    }
    public void DrainFemi()
    {
        string drainText = "";
        drainText += _enemy.DrainFemi();
        _textBox.text = drainText;
    }
}
