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
        GameObject over = hoverOver.transform.parent.gameObject;
        RectTransform rt = (RectTransform)over.transform;
        Vector3 vector3 = rt.position;
        if (rt.position.x < Screen.width / 2)
        {
            vector3.x += Screen.width * xDistance; // xDistance;
        }
        else
        {
            vector3.x -= Screen.width * xDistance;
        }
        if (rt.position.y < Screen.height / 2)
        {
            vector3.y += Screen.height * yDistance;
        }
        else
        {
            vector3.y -= Screen.height * yDistance;
        }
        Debug.Log(over.transform.position.x);
        Debug.Log(rt.rect.yMax);
        hoverRect.position = vector3;
    }

    public virtual void StopHovering()
    {
        hoverblock.SetActive(false);
    }
}