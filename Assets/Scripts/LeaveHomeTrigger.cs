using UnityEngine;

public class LeaveHomeTrigger : MonoBehaviour
{
    [SerializeField] private HomeMain home = null;
    private string playerTag;

    private void Start() => playerTag = PlayerHolder.GetTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            home.LeaveHome();
        }
    }
}