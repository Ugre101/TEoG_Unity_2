using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeaveSex : MonoScene
{
    private GameUI gameUI;
    private Button btn;
    public void Start()
    {
        gameUI = GetComponentInParent<GameUI>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Leave);
    }
    public void Leave()
    {
        gameUI.Resume();
    }

}
