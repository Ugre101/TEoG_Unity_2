using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHomeTrigger : MonoBehaviour
{
    public GameUI GameUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameUI.EnterHome();
    }
   
}
