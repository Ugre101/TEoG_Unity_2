using UnityEngine;
using UnityEngine.EventSystems;

public class PerkTreeController : MonoBehaviour, IScrollHandler
{
    public KeyBindings keyBindings;
    public GameObject toZoom;
    public RectTransform zoomRect;
    public PerkButton[] perkButtons;
    public BasicStatButton[] statButtons;
    [Range(0.5f, 3f)]
    [SerializeField]
    private float zoom = 1f;

    public float setZoom
    {
        get { return zoom; }
        set
        {
            zoom = Mathf.Clamp(value, 0.5f, 3f);
            zoomRect.localScale = new Vector3(zoom, zoom, zoom);
        }
    }
    private void Start()
    {
        statButtons = GetComponentsInChildren<BasicStatButton>();
        perkButtons = GetComponentsInChildren<PerkButton>();
        Debug.Log(statButtons.Length);
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(keyBindings.zoomInKey))
        {
            setZoom += 0.05f;
        }
        else if (Input.GetKey(keyBindings.zoomOutKey))
        {
            setZoom -= 0.05f;
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
            Debug.Log(Event.current.delta);
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