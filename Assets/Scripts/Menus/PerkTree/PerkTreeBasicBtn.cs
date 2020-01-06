using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PerkTreeBasicBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool Taken { get; protected set; } = false;

    [SerializeField]
    protected PlayerMain player = null;

    [SerializeField]
    protected TextMeshProUGUI amount = null;

    [SerializeField]
    protected PerkInfo perkInfo = null;

    [SerializeField]
    protected Button btn = null;

    [SerializeField]
    protected PerkTreeHoverText hoverText = null;

    public Image rune;
    protected Color color;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // if not assing, then assing hover remember to not become to lazy as it can affect perfomance(I think).
        if (btn == null)
        {
            btn = GetComponent<Button>();
        }
        btn.onClick.AddListener(Use);
        if (hoverText == null)
        {
            hoverText = GetComponentInParent<PerkTreeHoverText>();
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
        }
    }

    public virtual void OnEnable()
    {
        RuneOpacity();
    }

    public void RuneOpacity()
    {
        color = rune.color;
        color.a = Taken ? 1f : 0.5f;
        rune.color = color;
    }

    public virtual void Use()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (perkInfo != null)
        {
            PerkTreeHoverText.Hovering(perkInfo.Info);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PerkTreeHoverText.StopHovering();
    }
}