using UnityEngine;

public class EnterHomeTrigger : MonoBehaviour
{
    public CanvasMain GameUI;

    private void Start()
    {
        if (GameUI == null)
        {
            GameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<CanvasMain>();
            Debug.LogError("You forgot to assing a " + this);
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