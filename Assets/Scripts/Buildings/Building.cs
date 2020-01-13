using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    protected PlayerMain player;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = player != null ? player : PlayerMain.GetPlayer;
    }
}