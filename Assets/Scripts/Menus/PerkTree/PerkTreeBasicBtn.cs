using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PerkTreeBasicBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool taken = false;

    public bool Taken
    {
        get => taken;
        protected set
        {
            taken = value;
            RuneOpacity();
        }
    }

    [SerializeField] protected PlayerMain player = null;

    [SerializeField] protected TextMeshProUGUI amount = null;

    [SerializeField] protected PerkInfo perkInfo = null;

    [SerializeField] protected Button btn = null;

    [SerializeField] protected Image rune = null;

    protected Color color;

    protected void SetRuneColor(float value)
    {
        color = rune.color;
        color.a = value;
        rune.color = color;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        // if not assing, then assing hover remember to not become to lazy as it can affect perfomance(I think).
        btn = btn != null ? btn : GetComponent<Button>();
        btn.onClick.AddListener(Use);
        player = player != null ? player : PlayerMain.GetPlayer;
    }

    public virtual void OnEnable() => RuneOpacity();

    public void RuneOpacity() => SetRuneColor(Taken ? 1f : 0.5f);

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