using UnityEngine;
using UnityEngine.EventSystems;

public class PerkTreeController : MonoBehaviour, IScrollHandler
{
    private PlayerMain player => PlayerHolder.Player;

    [SerializeField] private RectTransform zoomRect = null;

    [SerializeField] private GameObject vorePerksTree = null;

    [SerializeField] private RectTransform perkRect = null;

    private float zoom = 2f;

    [SerializeField] private float touchpadZoomSen = 0.05f, keyZooomSen = 0.05f;

    private bool hasTouch = false;

    public float SetZoom
    {
        get => zoom;
        set
        {
            zoom = Mathf.Clamp(value, 1f, 5f);
            zoomRect.localScale = new Vector3(zoom, zoom, zoom);
        }
    }

    private void OnEnable()
    {
        if (vorePerksTree != null)
        {
            vorePerksTree.SetActive(player.Vore.Active);
        }
        perkRect.localPosition = new Vector3(0, 0, 0);
        hasTouch = Input.touchSupported;
    }

    // Update is called once per frame
    private void Update()
    {
        if (KeyBindings.ZoomInKey.GetsKey)
        {
            SetZoom += keyZooomSen;
        }
        else if (KeyBindings.ZoomOutKey.GetsKey)
        {
            SetZoom -= keyZooomSen;
        }
        if (hasTouch)
        {
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
                //  TODO Test is works
                SetZoom += deltaMagnitudeDiff * touchpadZoomSen;
            }
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        //  Debug.Log("scroll");
    }

    public void OnGUI()
    {
        if (Event.current.type == EventType.ScrollWheel)
        {
            float y = Event.current.delta.y;
            float scrollValue = Mathf.Abs(y) * touchpadZoomSen;
            if (y < 0)
            {
                SetZoom -= scrollValue;
            }
            else if (y > 0)
            {
                SetZoom += scrollValue;
            }
        }
    }
}