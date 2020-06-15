using UnityEngine;
using UnityEngine.Tilemaps;

public class CanTelePortTo : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private WorldMaps worldMaps;
    [SerializeField] private Sprite deActivated = null;
    [SerializeField] private bool know = false, walkOnToUnlock = true;
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
    public Vector3 LandCordinations => transform.position;
    public WorldMaps World { get => worldMaps; private set => worldMaps = value; }

    public void Load(bool know) => Know = know;

    public void Unlock() => Know = true;

    private bool justTeleportedTo = false;

    public void TeleportTo() => justTeleportedTo = true;

    private float timeLoaded;

    private void Start()
    {
        Map = GetComponentInParent<Map>().gameObject.GetComponent<Tilemap>();
        World = GetComponentInParent<WorldMap>().World;
        spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        animator = animator != null ? animator : GetComponent<Animator>();
        HandleSprite();
        MapEvents.TileMapChange += NewMapIsThisMap;
        Save.LoadEvent += OnLoad;
    }

    private void OnLoad()
    {
        NewMapIsThisMap(MapEvents.CurrentMap);
        justTeleportedTo = false;
        timeLoaded = Time.unscaledTime;
    }

    private void NewMapIsThisMap(Tilemap tilemap)
    {
        if (tilemap == map)
        {
            HandleSprite();
        }
        else
        {
            DisabledTeleport();
        }
    }

    private void HandleSprite()
    {
        if (Know)
        {
            animator.enabled = true;
            animator.Play("Spinning");
        }
        else
        {
            DisabledTeleport();
        }
    }

    private void DisabledTeleport()
    {
        animator.enabled = false;
        spriteRenderer.sprite = deActivated;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerHolder.GetTag))
        {
            if (!Know && walkOnToUnlock)
            {
                Know = true;
            }
            else if (!Know && !walkOnToUnlock)
            {
                PlayerHolder.Player.Events.SoloEvents.TeleportIsLocked();
            }
            if (Know)
            {
                if (!justTeleportedTo && timeLoaded + 1f <= Time.unscaledTime)
                {
                    WalkedOnTeleport?.Invoke();
                    // CanvasMain.GetCanvasMain.TeleportMenu();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerHolder.GetTag))
        {
            if (justTeleportedTo)
            {
                justTeleportedTo = false;
            }
        }
    }

    public TeleportSave SaveThis() => new TeleportSave(Map.name, World, Know);

    public void LoadThis(TeleportSave save)
    {
        if (World == save.World)
        {
            if (Map.name == save.MapName)
            {
                Know = save.Know;
            }
        }
    }

    public delegate void WalktOnTeleport();

    public static event WalktOnTeleport WalkedOnTeleport;
}

[System.Serializable]
public struct TeleportSave
{
    [SerializeField] private string mapName;
    [SerializeField] private WorldMaps world;
    [SerializeField] private bool know;

    public TeleportSave(string mapName, WorldMaps world, bool know)
    {
        this.mapName = mapName;
        this.world = world;
        this.know = know;
    }

    public string MapName => mapName;
    public WorldMaps World => world;
    public bool Know => know;
}