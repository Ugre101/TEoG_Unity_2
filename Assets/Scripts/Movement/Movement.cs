using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    // Public
    [Range(1f, 20f)]
    public float movementSpeed = 7.0f;

    public CanvasMain canvas;
    public EnemySpawner spawner;

    // Private
    private Tilemap _map;

    private Tilemap lastMap;

    [SerializeField]
    private GameObject pointer = null;

    [SerializeField]
    private BoxCollider2D _coll = null;

    [SerializeField]
    private Rigidbody2D _rb2d = null;

    private Vector2 CurPos { get => _rb2d.position; set => _rb2d.position = value; }
    private bool first = false;
    private Vector2 target;

    private Vector2 Target
    {
        get => target;
        set
        {
            first = true;
            target = value;
        }
    }

    private float _xMax, _xMin, _yMin, _yMax;
    private bool mobilePlatform, touchSupport, mousePresent;
    private Vector3 lookRight = new Vector3(0, 0, 0), lookLeft = new Vector3(0, 180, 0);

    // Start is called before the first frame update
    private void Start()
    {
        mobilePlatform = Application.isMobilePlatform;
        touchSupport = Input.touchSupported;
        mousePresent = Input.mousePresent;
        _map = MapEvents.CurrentMap;
        MapEvents.WorldMapChange += DoorChanged;
        if (_rb2d == null)
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }
        if (_coll == null)
        {
            _coll = GetComponent<BoxCollider2D>();
        }
    }

    // Update for player input
    private void Update()
    {
        if (mousePresent)
        {
            // Mouse left hold
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            // Cancel mouse left hold
            else if (Input.GetMouseButtonUp(0))
            {
                Target = CurPos;
            }
            // Mouse right click
            else if (Input.GetMouseButtonDown(1))
            {
                Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(pointer, Target, Quaternion.identity);
            }
        }
        // WASD and Arrow keys
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            // Current
            Vector2 _pos = CurPos;

            float movVert = Input.GetAxis("Vertical");
            float movHori = Input.GetAxis("Horizontal");
            _pos.x += movHori;
            _pos.y += movVert;
            Target = _pos;
        }
        if (touchSupport || mobilePlatform)
        {
            // Not tested
            if (Input.touchCount == 1)
            {
                Touch t = Input.GetTouch(0);
                Target = Camera.main.WorldToScreenPoint(t.position);
            }
        }
    }

    // FixedUpdate for movement
    private void FixedUpdate()
    {
        // Clamp player inside tilemap & handle all player movement in one place to ease clamping
        if (_map != lastMap)
        {
            TilemapLimits();
        }
        if (CurPos != Target && first)
        {
            transform.eulerAngles = Target.x - CurPos.x > 0 ? lookRight : lookLeft;
            Target = new Vector2(Mathf.Clamp(Target.x, _xMin, _xMax), Mathf.Clamp(Target.y, _yMin, _yMax));
            CurPos = Vector2.MoveTowards(CurPos, Target, movementSpeed * Time.fixedDeltaTime);
        }
    }

    private void TilemapLimits()
    {
        Vector3 minTile = _map.CellToWorld(_map.cellBounds.min);
        Vector3 maxTile = _map.CellToWorld(_map.cellBounds.max);
        _xMin = minTile.x;
        _xMax = maxTile.x;

        _yMin = minTile.y;
        _yMax = maxTile.y;

        lastMap = _map;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyPrefab _colEnemy = collision.gameObject.GetComponent<EnemyPrefab>();
            canvas.StartCombat(_colEnemy);
            if (spawner != null)
            {
                spawner.RePosistion(collision.gameObject);
            }
        }
    }

    private void DoorChanged() => _map = MapEvents.CurrentMap;
}