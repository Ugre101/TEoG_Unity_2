using UnityEngine;

public class EnterHomeTrigger : MonoBehaviour
{
    [SerializeField] private HomeCanvas homeCanvas = null;

    private void Start() => homeCanvas = homeCanvas != null ? homeCanvas : HomeCanvas.GetHomeCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerHolder.GetTag))
        {
            homeCanvas.EnterHome();
        }
    }
}