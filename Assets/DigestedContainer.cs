using UnityEngine;

public class DigestedContainer : MonoBehaviour
{
    public static DigestedContainer GetContainer => thisContainer;
    private static DigestedContainer thisContainer;

    private void Awake()
    {
        if (thisContainer == null)
        {
            thisContainer = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}