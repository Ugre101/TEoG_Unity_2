using UnityEngine;

public class FollowMouseUI : MonoBehaviour
{
    public bool Active = true;

    [Range(0, 100)]
    public float xDist = 0, yDist = 0;

    [Range(0, 100)]
    public int sensitivity = 5;

    private Vector3 lastPos;
    public bool FluidPivot = false;
    public RectTransform rect;

    [Header("Pivot settings")]
    [Range(0, 1f)]
    public float width;

    [Range(0, 1f)]
    public float height;
    private void Start()
    {
        if (rect == null) rect = GetComponent<RectTransform>();
    }
    private void LateUpdate()
    {
        Vector3 newPos = Input.mousePosition;
        if (Active)
        {
            // Makes text move around slight less, making it easier to read.
            Vector3 diff = newPos - lastPos;
            if (Mathf.Abs(diff.x) > sensitivity || Mathf.Abs(diff.y) > sensitivity)
            {
                Vector3 pos = newPos;
                lastPos = Input.mousePosition;
                pos.x += xDist;
                pos.y += yDist;
                transform.position = pos;
                if (FluidPivot)
                {
                    Vector2 newPiv = rect.pivot;
                    if (newPos.x > Screen.width * width)
                    {
                        newPiv.x = 1;
                        pos.x -= xDist * 2;
                    }
                    else
                    {
                        newPiv.x = 0;
                    }
                    if (newPos.y > Screen.height)
                    {
                        newPiv.y = 1;
                        pos.y -= yDist * 2;
                    }
                    else
                    {
                        newPiv.y = 0;
                    }
                    if (newPiv.y > Screen.height * 0.7f)
                    {
                    }
                    rect.pivot = newPiv;
                }
            }
        }
    }
}