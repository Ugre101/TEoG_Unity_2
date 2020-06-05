using UnityEngine;

public class PopupHandler : MonoBehaviour
{
    public static PopupHandler GetPopupHandler { get; protected set; }
    [SerializeField] private TimedPopupText timedPopupText = null;

    private void Awake()
    {
        if (GetPopupHandler == null)
        {
            GetPopupHandler = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnTimedPopup(string message) => Instantiate(timedPopupText, transform).Setup(message);

    public void SpawnTimedPopup(string message, float time) => Instantiate(timedPopupText, transform).Setup(message, time);
}