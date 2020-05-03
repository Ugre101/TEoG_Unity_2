using UnityEngine;
using UnityEngine.Tilemaps;

public class CanTelePortTo : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private Vector3 landPlatform;
    [SerializeField] private WorldMaps worldMaps;
    [SerializeField] private Sprite deActivated = null;
    [SerializeField] private bool know = true;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Animator animator = null;

    public bool Know
    {
        get => know;
        private set
        {
            know = value;
            HandleSprite();
        }
    }

    public Tilemap Map { get => map; private set => map = value; }
    public Vector3 LandPlatform { get => landPlatform; private set => landPlatform = value; }
    public WorldMaps WorldMaps { get => worldMaps; private set => worldMaps = value; }

    public void Load(bool know) => Know = know;

    private void Start()
    {
        Map = GetComponentInParent<Map>().gameObject.GetComponent<Tilemap>();
        LandPlatform = transform.position;
        WorldMaps = GetComponentInParent<WorldMap>().World;
        spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        animator = animator != null ? animator : GetComponent<Animator>();
        HandleSprite();
    }

    private void HandleSprite()
    {
        if (Know)
        {
            animator.Play("Spinning");
        }
        else
        {
            if (deActivated != null)
            {
                spriteRenderer.sprite = deActivated;
            }
            else
            {
                Debug.LogError("A teleporter is missing sprites, world: " + WorldMaps + " and Map: " + Map.name);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerMain.GetTag))
        {
            Debug.Log("Player");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerMain.GetTag))
        {
            Debug.Log("Player Trigger");
            // TODO if just teleported don't open teleport menu
        }
    }
}