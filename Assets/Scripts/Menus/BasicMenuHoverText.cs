using TMPro;
using UnityEngine;

public class BasicMenuHoverText : MonoBehaviour
{
    public GameObject hoverblock;
    public TextMeshProUGUI hovertext;

    [SerializeField]
    [Range(0f, 0.5f)]
    private float xDistance = 0.123f;

    [SerializeField]
    [Range(0f, 0.5f)]
    private float yDistance = 0.123f;

    private RectTransform Parent;
    private RectTransform hoverRect;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Parent = (RectTransform)transform;
        hoverRect = (RectTransform)hoverblock.transform;
    }

    public virtual void Hovering(GameObject hoverOver)
    {
        hoverblock.SetActive(true);
        RectTransform rt = (RectTransform)hoverOver.transform;
        float width = rt.rect.width / 2 + hoverRect.rect.width / 2;
        float height = rt.rect.height / 2 + hoverRect.rect.height / 2;
        Vector3 vector3 = rt.localPosition;
        if (rt.position.x < Screen.width / 2)
        {
            vector3.x += width; // xDistance;
        }
        else
        {
            vector3.x -= width; // xDistance;
        }
        if (rt.position.y < Screen.height / 2)
        {
            vector3.y += height; // yDistance;
        }
        else
        {
            vector3.y -= height; // yDistance;
        }
        Debug.Log(height + " " + width);
        Debug.Log(vector3);
        hoverRect.localPosition = vector3;
    }

    public virtual void StopHovering()
    {
        hoverblock.SetActive(false);
    }
}