using TMPro;
using UnityEngine;

public class BasicMenuHoverText : MonoBehaviour
{
    public GameObject hoverblock;
    public TextMeshProUGUI hovertext;

    [SerializeField]
    [Range(0f, 50f)]
    private float xDistance = 0.123f;

    [SerializeField]
    [Range(0f, 50f)]
    private float yDistance = 0.123f;

    private RectTransform Parent;
    private RectTransform hoverRect;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Parent = (RectTransform)transform;
        hoverRect = (RectTransform)hoverblock.transform;
    }

    public virtual void Hovering(GameObject hoverOver,Vector2 mousePos)
    {
        hoverblock.SetActive(true);
        /*
        Vector2 vector2 = mousePos;
        vector2.x += xDistance;
        vector2.y += yDistance;
        hoverRect.position = vector2;
        */
    }

    public virtual void StopHovering()
    {
        hoverblock.SetActive(false);
    }
}