using UnityEngine;
using UnityEngine.EventSystems;

public class PerkTreeController : MonoBehaviour, IScrollHandler
{
    [SerializeField] private PlayerMain player = null;

    [SerializeField] private KeyBindings keyBindings = null;

    [SerializeField] private RectTransform zoomRect = null;

    [SerializeField] private GameObject vorePerksTree = null;

    [SerializeField] private RectTransform perkRect = null;

    private float zoom = 2f;

    [SerializeField] private float touchpadZoomSen = 0.05f;

    [SerializeField] private float keyZooomSen = 0.05f;

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
    }

    // Update is called once per frame
    private void Update()
    {
        if (keyBindings.zoomInKey.GetKey())
        {
            SetZoom += keyZooomSen;
        }
        else if (keyBindings.zoomOutKey.GetKey())
        {
            SetZoom -= keyZooomSen;
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