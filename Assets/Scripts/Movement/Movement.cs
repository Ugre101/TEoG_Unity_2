using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    // Public
    [Range(1f, 20f)]
    [SerializeField] private float movementSpeed = 7.0f;

    [Range(0.1f, 2f)]
    [SerializeField] private float bottomOffset = 1f;

    // Private
    [SerializeField] private GameObject pointer = null;

    [SerializeField] private BoxCollider2D _coll = null;

    [SerializeField] private Rigidbody2D _rb2d = null;

    private Vector2 CurPos { get => _rb2d.position; set => _rb2d.position = value; }
    private float Vertical => Input.GetAxis("Vertical");
    private float Horizontal => Input.GetAxis("Horizontal");
    private Vector3 MousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private bool clickedOnce = false;
    private Vector2 target;

    private Vector2 Target
    {
        get => target;
        set
        {
            clickedOnce = true;
            target = value;
        }
    }

    private float _xMax, _xMin, _yMin, _yMax;
    private bool mobilePlatform, touchSupport, mousePresent;
    private Vector3 lookRight = new Vector3(0, 0, 0), lookLeft = new Vector3(0, 180, 0);

    // Start is called before the first frame update
    private void Start()
    {
        _rb2d = _rb2d != null ? _rb2d : GetComponent<Rigidbody2D>();
        _coll = _coll != null ? _coll : GetComponent<BoxCollider2D>();

        mobilePlatform = Application.isMobilePlatform;
        touchSupport = Input.touchSupported;
        mousePresent = Input.mousePresent;
        MapEvents.WorldMapChange += TilemapLimits;
        TilemapLimits(MapEvents.CurrentMap);
    }

    // Update for player input
    private void Update()
    {
        if (mousePresent)
        {
            // Mouse left hold
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Target = MousePos;
            }
            // Cancel mouse left hold
            else if (Input.GetMouseButtonUp(0))
            {
                Target = CurPos;
            }
            // Mouse right click
            else if (Input.GetMouseButtonDown(1))
            {
                Target = MousePos;
                Instantiate(pointer, Target, Quaternion.identity);
            }
        }
        // WASD and Arrow keys
        if (Vertical != 0 || Horizontal != 0)
        {
            Target = new Vector2(CurPos.x + Horizontal, CurPos.y + Vertical);
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
        if (CurPos != Target && clickedOnce)
        {
            // look at right direction
            transform.eulerAngles = Target.x - CurPos.x > 0 ? lookRight : lookLeft;
            // clamp player inside map
            Target = new Vector2(Mathf.Clamp(Target.x, _xMin, _xMax), Mathf.Clamp(Target.y, _yMin, _yMax));
            CurPos = Vector2.MoveTowards(CurPos, Target, movementSpeed * Time.fixedDeltaTime);
        }
    }

    private void TilemapLimits(Tilemap _map)
    {
        Vector3 minTile = _map.CellToWorld(_map.cellBounds.min);
        Vector3 maxTile = _map.CellToWorld(_map.cellBounds.max);
        _xMin = minTile.x;
        _xMax = maxTile.x;

        _yMin = minTile.y + bottomOffset;
        _yMax = maxTile.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyPrefab _colEnemy = collision.gameObject.GetComponent<EnemyPrefab>();
            TriggerEnemy?.Invoke(_colEnemy);
        }
    }

    public static event Action<BasicChar> TriggerEnemy;
}