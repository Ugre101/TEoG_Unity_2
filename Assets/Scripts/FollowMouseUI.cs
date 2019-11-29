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
                    newPiv.x = newPos.x > Screen.width * width ? 1 : 0;
                    newPiv.y = newPos.y > Screen.height * height ? 0 : 1;
                    rect.pivot = newPiv;
                }
            }
        }
    }
}