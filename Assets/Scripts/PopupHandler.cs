using UnityEngine;

public class PopupHandler : MonoBehaviour
{
    public static PopupHandler GetPopupHandler { get; protected set; }
    [SerializeField] private TimedPopupText timedPopupText = null;
    [SerializeField] private CloseButtonPoputText closeBtnPopupText = null;

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

    public void SpawnBtnPopup(string message) => Instantiate(closeBtnPopupText, transform).Setup(message);

    public void SpawnTimedPopup(string message) => Instantiate(timedPopupText, transform).Setup(message);

    public void SpawnTimedPopup(string message, float time) => Instantiate(timedPopupText, transform).Setup(message, time);

    public void DelayedSpawnTimedPopup(string message, float time = 3f) => StartCoroutine(UgreTools.WaitAFrameAndExecute(() => SpawnTimedPopup(message, time)));//WaitAFrame(message, time));
}