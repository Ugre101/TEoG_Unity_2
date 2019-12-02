using TMPro;
using UnityEngine;

public class BasicMenuHoverText : MonoBehaviour
{
    public GameObject hoverblock;
    public TextMeshProUGUI hovertext;

    // Start is called before the first frame update
    public virtual void Start()
    {
        StopHovering();
    }

    private void OnDisable()
    {
        StopHovering();
    }

    public virtual void Hovering(GameObject hoverOver, Vector2 mousePos)
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