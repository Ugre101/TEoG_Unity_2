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
    protected Image rune = null;

    protected Color color;

    protected float RuneColor
    {
        set
        {
            color = rune.color;
            color.a = value;
            rune.color = color;
        }
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        // if not assing, then assing hover remember to not become to lazy as it can affect perfomance(I think).
        if (btn == null)
        {
            btn = GetComponent<Button>();
        }
        btn.onClick.AddListener(Use);
        player = player != null ? player : PlayerMain.GetPlayer;
    }

    public virtual void OnEnable() => RuneOpacity();

    public void RuneOpacity() => RuneColor = Taken ? 1f : 0.5f;

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

    public void OnPointerExit(PointerEventData eventData) => PerkTreeHoverText.StopHovering();
}