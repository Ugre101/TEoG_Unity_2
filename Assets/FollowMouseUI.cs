using UnityEngine;

public class FollowMouseUI : MonoBehaviour
{
    public bool Active = true;

    [Range(0, 100)]
    public float xDist = 0, yDist = 0;

    [Range(0, 100)]
    public int sensitivity = 5;

    private Vector3 lastPos;

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
            }
        }
    }
}