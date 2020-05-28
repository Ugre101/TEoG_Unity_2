using UnityEngine;

public class DigestedContainer : MonoBehaviour
{
    public static DigestedContainer GetContainer { get; private set; }

    private void Awake()
    {
        if (GetContainer == null)
        {
            GetContainer = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void AddTo(Transform digested)
    {
        digested.SetParent(GetContainer.transform);
        // Set inactive?
        if (GetContainer.transform.childCount > 20)
        {
            // Clear??
        }
    }
}