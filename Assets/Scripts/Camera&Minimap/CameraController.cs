using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    // Public
    [SerializeField] private Transform player = null;

    [FormerlySerializedAs("_smoothing")] [Header("Settings")] [SerializeField] private float smoothing = 1f;

    [FormerlySerializedAs("_maxCam")] [SerializeField] private float maxCam = 20f;

    [Range(0.01f, 0.1f)] [SerializeField] private float zoomSpeed = 0.1f;

    [Tooltip("Less is more, changes how much out of map camera can see")] [Range(0.1f, 0.6f)] [SerializeField]
    private float viewLimit = 0.4f;

    [FormerlySerializedAs("_offset")] [SerializeField] private Vector3 offset = new Vector3(1f, 0, -10);

    // Private
    private Tilemap map;

    private Camera cam;
    private float xMax, xMin, yMin, yMax;

    private float preferadSize;
    private float OrthSize { get; set; } = 14f;

    private void YouSetOrthSize(float value)
    {
        SetOrthSize(value);
        preferadSize = OrthSize; // after auto scaling reset to prefered.
    }

    private void SetOrthSize(float value)
    {
        OrthSize = Mathf.Clamp(value, 4, maxCam);
        cam.orthographicSize = OrthSize;
        TilemapLimits();
    }

    private Vector3 minTile, maxTile;

    // Start is called before the first frame update
    private void Start()
    {
        player = player != null ? player : PlayerSprite.Instance.transform;
        map = MapEvents.CurrentMap;
        MapEvents.TileMapChange += DoorChanged;
        cam = GetComponent<Camera>();
        minTile = map.CellToWorld(map.cellBounds.min);
        maxTile = map.CellToWorld(map.cellBounds.max);
        YouSetOrthSize(14f);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 target = player.transform.position + offset;
        target.x = Mathf.Clamp(target.x, xMin, xMax);
        target.y = Mathf.Clamp(target.y, yMin, yMax);
        // if Camera controll and check if bigger than tilemap
        if (GameManager.CurState == GameState.Free)
        {
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

                // ... change the orthographic size based on the change in distance between the touches.
                YouSetOrthSize(OrthSize + deltaMagnitudeDiff * zoomSpeed);
            }

            float scrollValue = Input.GetAxis("Mouse ScrollWheel");
            if (KeyBindings.ZoomInKey.GetsKey)
            {
                YouSetOrthSize(OrthSize - zoomSpeed);
            }
            else if (KeyBindings.ZoomOutKey.GetsKey)
            {
                YouSetOrthSize(OrthSize + zoomSpeed);
            }
            else if (Math.Abs(scrollValue) > 0.1f)
            {
                YouSetOrthSize(OrthSize - scrollValue); // times zoom speed
            }
        }

        transform.position = Vector3.Lerp(transform.position, target, smoothing);
        if (OutOfBorder(target))
        {
            SetOrthSize(OrthSize - (zoomSpeed * 2));
        }
        else if (preferadSize != OrthSize && CanExpand(target))
        {
            SmoothZoom(preferadSize);
        }
    }

    private void SmoothZoom(float targetValue)
    {
        if (OrthSize < targetValue)
        {
            SetOrthSize(OrthSize + zoomSpeed);
        }
        else if (OrthSize > targetValue)
        {
            SetOrthSize(OrthSize - zoomSpeed);
        }
    }

    private bool OutOfBorder(Vector3 vector3)
    {
        if (vector3.x < xMin)
        {
            return true;
        }
        else if (vector3.x > xMax)
        {
            return true;
        }
        else if (vector3.y < yMin)
        {
            return true;
        }
        else if (vector3.y > yMax)
        {
            return true;
        }

        return false;
    }

    private bool CanExpand(Vector3 vector3)
    {
        float margin = 2;
        if (vector3.x - margin < xMin)
        {
            return false;
        }
        else if (vector3.x + margin > xMax)
        {
            return false;
        }
        else if (vector3.y - margin < yMin)
        {
            return false;
        }
        else if (vector3.y + margin > yMax)
        {
            return false;
        }

        return true;
    }

    private void TilemapLimits()
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        xMin = minTile.x + (width * viewLimit);
        xMax = maxTile.x - (width * viewLimit);

        yMin = minTile.y + (height * viewLimit);
        yMax = maxTile.y - (height * viewLimit);

        //_maxCam = Mathf.Min((maxTile.y - minTile.y) / 2f, (maxTile.x - minTile.x) / (cam.aspect * 2f));
    }

    private void DoorChanged(Tilemap tilemap)
    {
        map = tilemap;
        minTile = map.CellToWorld(map.cellBounds.min);
        maxTile = map.CellToWorld(map.cellBounds.max);
        TilemapLimits();
    }
}