using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHomeTrigger : MonoBehaviour
{
    public CanvasMain GameUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameUI.EnterHome();
        }
    }
   
}
