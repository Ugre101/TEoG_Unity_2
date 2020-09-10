using UnityEngine;

public class SkippedEvents : MonoBehaviour
{
    [SerializeField] private SkippedEvent skippedEvent = null;
    [SerializeField] private Transform container = null;

    private void OnEnable()
    {
        if (skippedEvent != null && container != null)
        {
            ListEvents();
        }
        else
        {
            Debug.LogError("Missing references in SkippedEvents");
        }
    }

    private void ListEvents()
    {
        container.KillChildren();
        ToggleEvent("Need to shit", NeedToShit.SkipEvent);
        ToggleEvent("Give birth", GiveBirth.SkipEvent);
    }

    private void ToggleEvent(string title, SkipEvent skipEvent) => Instantiate(skippedEvent, container).Setup(title, skipEvent);
}