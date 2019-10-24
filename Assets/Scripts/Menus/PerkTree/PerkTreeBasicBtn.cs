using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PerkTreeBasicBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool taken = false;
    public playerMain player;
    public TextMeshProUGUI amount;
    public PerkInfo perkInfo;
    public   Button btn;
    private PerkTreeHoverText hoverText;
    public Image rune;
    protected Color color;
    // Start is called before the first frame update
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Use);
        hoverText = GetComponentInParent<PerkTreeHoverText>();
    }
    public virtual void OnEnable()
    {
        color = rune.color;
        color.a = taken ? 1f : 0.5f;
        rune.color = color;
    }
    public virtual void Use()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverText.Hovering(gameObject,eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverText.StopHovering();
    }
}