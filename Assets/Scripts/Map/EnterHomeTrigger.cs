using UnityEngine;

public class EnterHomeTrigger : MonoBehaviour
{
    [SerializeField]
    private CanvasMain GameUI = null;

    private void Start()
    {
        if (GameUI == null)
        {
            GameUI = CanvasMain.GetCanvasMain;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerMain.GetPlayer.tag))
        {
            GameUI.EnterHome();
        }
    }
}