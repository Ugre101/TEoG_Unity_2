using UnityEngine;

public class BullyMark : MonoBehaviour
{
    public static BullyMark Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}