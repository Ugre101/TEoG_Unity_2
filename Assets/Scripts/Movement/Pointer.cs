using UnityEngine;

public class Pointer : MonoBehaviour
{
    [Range(0,5)]
    public float despawnTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, despawnTime);
    }

}
