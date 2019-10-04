using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    // Public
    [Range(1f,20f)]
    public float movementSpeed = 7.0f;

    public GameObject pointer;
    public GameUI canvas;
    public EnemySpawner spawner;
    public MapEvents mapEvents;

    // Private
    private Tilemap _map;
    private BoxCollider2D _coll;
    private Rigidbody2D _rb2d;
    private Vector2 _target;
    private float _xMax, _xMin, _yMin, _yMax;
    private bool _move = false;
    private EnemyPrefab _colEnemy;

    // Start is called before the first frame update
    private void Start()
    {
        _map = mapEvents.CurrentMap;
        MapEvents.WorldMapChange += DoorChanged;
        _rb2d = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_move == false)
        {
            _target = _rb2d.position;
        }
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // Mouse hold
            _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _move = false;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // Mouse set target
            _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(pointer, _target, Quaternion.identity);
            _move = true;
        }
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            //Keyboard
            Vector2 _pos = _rb2d.position;

            float movVert = Input.GetAxis("Vertical");
            float movHori = Input.GetAxis("Horizontal");

            _pos.x += movHori * movementSpeed * Time.deltaTime;
            _pos.y += movVert * movementSpeed * Time.deltaTime;
            _target = _pos;
            _move = false;
        }
        // Clamp player inside tilemap
        Vector3 minTile = _map.CellToWorld(_map.cellBounds.min);
        Vector3 maxTile = _map.CellToWorld(_map.cellBounds.max);
        tilemapLimits(minTile, maxTile);
        _target.x = Mathf.Clamp(_target.x, _xMin, _xMax);
        _target.y = Mathf.Clamp(_target.y, _yMin, _yMax);
        // Handle all player movement in one place to ease clamping
        if (_rb2d.position != _target)
        {
            _rb2d.position = Vector2.MoveTowards(_rb2d.position, _target, movementSpeed * Time.deltaTime);
        }
        // Reset _target if player stops holding mouse 0
    }

    private void tilemapLimits(Vector3 minTile, Vector3 maxTile)
    {
        _xMin = minTile.x;
        _xMax = maxTile.x;

        _yMin = minTile.y;
        _yMax = maxTile.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
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