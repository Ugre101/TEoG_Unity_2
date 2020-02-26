using UnityEngine;
using UnityEngine.Tilemaps;

public class HomeMapHandler : MonoBehaviour
{
    [SerializeField] private Tilemap startLawn = null, expandedLawn = null;
    [SerializeField] private Tilemap startLandPlatform = null, expandedLandPlatform = null;

    public Tilemap GetActiveLawn
    {
        get
        {
            gameObject.SetActive(true);
            ChoiceLawn();
            if (expandedLawn.gameObject.activeSelf)
            {
                return expandedLawn;
            }
            else
            {
                return startLawn;
            }
        }
    }

    public Tilemap HasLandPlatform()
    {
        if (GetActiveLawn == expandedLawn)
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
            startLawn.gameObject.SetActive(false);
            expandedLawn.gameObject.SetActive(true);
        }
        else
        {
            startLawn.gameObject.SetActive(true);
            expandedLawn.gameObject.SetActive(false);
        }
    }
}