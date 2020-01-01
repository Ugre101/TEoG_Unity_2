using UnityEngine;

public class EnterHomeTrigger : MonoBehaviour
{
    public CanvasMain GameUI;

    private void Start()
    {
        if (GameUI == null)
        {
            GameUI = CanvasMain.GetCanvasMain;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameUI.EnterHome();
        }
    }
}