using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    // Public
    public Transform _player;

    public MapEvents mapEvents;
    public KeyBindings keyBindings;

    // Private
    private Vector3 _offset = new Vector3(1f, 0, -10);

    private Tilemap _map;
    private Camera cam;
    private float _smoothing = 1f;
    private float _xMax, _xMin, _yMin, _yMax, _maxCam = 20f;
    private float _orthSize = 8f;

    // Start is called before the first frame update
    private void Start()
    {
        _map = mapEvents.CurrentMap;
        MapEvents.WorldMapChange += DoorChanged;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 minTile = _map.CellToWorld(_map.cellBounds.min);
        Vector3 maxTile = _map.CellToWorld(_map.cellBounds.max);
        tilemapLimits(minTile, maxTile);

        Vector3 _target = _player.transform.position + _offset;
        _target.x = Mathf.Clamp(_target.x, _xMin, _xMax);
        _target.y = Mathf.Clamp(_target.y, _yMin, _yMax);
        // if Camera controll and check if bigger than tilemap

        // Mobile zoom copied from unity learn
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...

            // ... change the orthographic size based on the change in distance between the touches.
            _orthSize += deltaMagnitudeDiff * 0.1f;

            // Make sure the orthographic size never drops below zero.
            _orthSize = Mathf.Max(_orthSize, 0.1f);
        }
        if (Input.GetKey(keyBindings.zoomInKey))
        {
            _orthSize -= 0.1f;
        }
        else if (Input.GetKey(keyBindings.zoomOutKey))
        {
            _orthSize += 0.1f;
        }
        else 
        {
            _orthSize -= Input.GetAxis("Mouse ScrollWheel");
        }
        _orthSize = Mathf.Clamp(_orthSize, 4, _maxCam);
        cam.orthographicSize = _orthSize;
        transform.position = Vector3.Lerp(transform.position, _target, _smoothing);
    }

    private void tilemapLimits(Vector3 minTile, Vector3 maxTile)
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        _xMin = minTile.x + width * 0.4f;
        _xMax = maxTile.x - width * 0.4f;

        _yMin = minTile.y + height * 0.4f;
        _yMax = maxTile.y - height * 0.4f;

        //_maxCam = Mathf.Min((maxTile.y - minTile.y) / 2f, (maxTile.x - minTile.x) / (cam.aspect * 2f));
    }

    private void DoorChanged()
    {
        _map = mapEvents.CurrentMap;
    }
}