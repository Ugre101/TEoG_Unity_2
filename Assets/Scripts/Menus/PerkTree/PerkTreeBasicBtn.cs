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
    public Button btn;
    public PerkTreeHoverText hoverText;
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
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMain>();
        }
    }

    public virtual void OnEnable()
    {
        RuneOpacity();
    }

    public void RuneOpacity()
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
        hoverText.Hovering(gameObject, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverText.StopHovering();
    }
    
}