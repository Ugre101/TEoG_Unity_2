using UnityEngine;
using UnityEngine.EventSystems;

public class PerkTreeController : MonoBehaviour, IScrollHandler
{
    public playerMain player;
    public KeyBindings keyBindings;
    public GameObject toZoom;
    public RectTransform zoomRect;
    public PerkButton[] perkButtons;
    public BasicStatButton[] statButtons;
    public GameObject vorePerksTree;
    public RectTransform perkRect;
    [Range(1f, 5f)]
    [SerializeField]
    private float zoom = 1f;

    private float touchpadZoomSen = 0.05f;
    private float keyZooomSen = 0.05f;

    public float setZoom
    {
        get { return zoom; }
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

    private void Start()
    {
        statButtons = GetComponentsInChildren<BasicStatButton>();
        perkButtons = GetComponentsInChildren<PerkButton>();
        foreach (BasicStatButton bsb in statButtons)
        {
            if (bsb.player == null)
            {
                bsb.player = player;
            }
        }
        foreach (PerkButton pb in perkButtons)
        {
            if (pb.player == null)
            {
                pb.player = player;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(keyBindings.zoomInKey))
        {
            setZoom += keyZooomSen;
        }
        else if (Input.GetKey(keyBindings.zoomOutKey))
        {
            setZoom -= keyZooomSen;
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        Debug.Log("scroll");
    }

    public void OnGUI()
    {
        if (Event.current.type == EventType.ScrollWheel)
        {
            float y = Event.current.delta.y;
            float abY = Mathf.Abs(y);
            if (y < 0)
            {
                setZoom -= abY * touchpadZoomSen;
            }
            else if (y > 0)
            {
                setZoom += abY * touchpadZoomSen;
            }
        }
    }

    public void Save()
    {
        // put arrays into save class and return
        // if and what is taken is what need to be saved
        // problem; how to id the perks
        // not necesarry; A way for future perk nerfs to be applied to player
    }

    public void Load()
    {
        // taken perks to be marked as taken
    }
}