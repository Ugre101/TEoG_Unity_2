using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    // Public
    [Range(1f, 20f)]
    public float movementSpeed = 7.0f;

    public GameUI canvas;
    public EnemySpawner spawner;
    public MapEvents mapEvents;

    // Private
    private Tilemap _map;

    private Tilemap lastMap;

    [SerializeField]
    private GameObject pointer;

    [SerializeField]
    private BoxCollider2D _coll;

    [SerializeField]
    private Rigidbody2D _rb2d;

    private Vector2 CurPos { get => _rb2d.position; set => _rb2d.position = value; }
    private Vector2 _target;
    private float _xMax, _xMin, _yMin, _yMax;
    private bool rightClick = false;
    private EnemyPrefab _colEnemy;
    private bool mobilePlatform, touchSupport, mousePresent;

    // Start is called before the first frame update
    private void Start()
    {
        mobilePlatform = Application.isMobilePlatform;
        touchSupport = Input.touchSupported;
        mousePresent = Input.mousePresent;
        _map = mapEvents.CurrentMap;
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
                _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            // Cancel mouse left hold
            else if (Input.GetMouseButtonUp(0))
            {
                _target = CurPos;
            }
            // Mouse right click
            else if (Input.GetMouseButtonDown(1))
            {
                _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(pointer, _target, Quaternion.identity);
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
            _target = _pos;
        }
        if (touchSupport || mobilePlatform)
        {
            // Not tested
            if (Input.touchCount == 1)
            {
                Touch t = Input.GetTouch(0);
                _target = Camera.main.WorldToScreenPoint(t.position);
            }
        }else
        {
        }
    }
    // FixedUpdate for movement
    private void FixedUpdate()
    {
        // Clamp player inside tilemap & handle all player movement in one place to ease clamping
        if (_map != lastMap)
        {
            tilemapLimits();
        }
        if (CurPos != _target)
        {
            _target.x = Mathf.Clamp(_target.x, _xMin, _xMax);
            _target.y = Mathf.Clamp(_target.y, _yMin, _yMax);
            CurPos = Vector2.MoveTowards(CurPos, _target, movementSpeed * Time.fixedDeltaTime);
        }
    }

    private void tilemapLimits()
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
            _colEnemy = collision.gameObject.GetComponent<EnemyPrefab>();
            canvas.StartCombat(_colEnemy);
            if (spawner != null)
            {
                spawner.RePosistion(collision.gameObject);
            }
        }
    }

    private void DoorChanged()
    {
        _map = mapEvents.CurrentMap;
    }
}