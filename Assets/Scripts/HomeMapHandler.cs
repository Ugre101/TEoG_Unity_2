using UnityEngine;
using UnityEngine.Tilemaps;

public class HomeMapHandler : MonoBehaviour
{
    [SerializeField] private GrownHomeProperty grownHome = null;
    [SerializeField] private Tilemap startLawn = null;
    [SerializeField] private Tilemap startLandPlatform = null, expandedLandPlatform = null;

    public Tilemap GetActiveLawn
    {
        get
        {
            gameObject.SetActive(true);
            ChoiceLawn();
            return startLawn;
        }
    }

    public Tilemap HasLandPlatform()
    {
        if (grownHome.AddedTiles.Count != 0)
        {
            return expandedLandPlatform;
        }
        else
        {
            return startLandPlatform;
        }
    }

    // Start is called before the first frame update
    private void Start() => Save.LoadEvent += ChoiceLawn;

    private void OnEnable() => ChoiceLawn();

    private void ChoiceLawn()
    {
        if (PlayerFlags.BeatBanditLord.Cleared)
        {
            Debug.Log("Grow");
            if (grownHome.AddedTiles.Count < 1)
            {
                Debug.Log("Grow; step 2");
                for (int i = 0; i < 10; i++)
                {
                    grownHome.GrowLawn();
                }
            }
        }
        else
        {
            if (grownHome.AddedTiles.Count != 0)
            {
                grownHome.ClearLawn();
            }
        }
    }
}