using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public static PlayerSprite Instance { get; private set; }
    public static string Tag => Instance.tag;
    public static Transform Transform => Instance.transform;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError("There was two instances of PlayerSprite");
        }
    }
}