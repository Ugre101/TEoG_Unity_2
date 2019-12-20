using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    protected PlayerMain player;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
        }
    }
}