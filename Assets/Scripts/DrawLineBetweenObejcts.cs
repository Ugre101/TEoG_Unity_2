using UnityEngine;

public class DrawLineBetweenObejcts : MonoBehaviour
{
    [SerializeField] private RectTransform lineRectTransform = null;
    [SerializeField] private RectTransform drawFrom = null, drawTo = null;

    // Start is called before the first frame update
    private void Start()
    {
        drawFrom = drawFrom != null ? drawFrom : GetComponent<RectTransform>();
        if (lineRectTransform == null)
        {
            enabled = false;
        }
        else if (drawTo == null)
        {
            lineRectTransform.gameObject.SetActive(false);
            enabled = false;
        }
        else
        {
            DrawLine();
        }
    }

    private void DrawLine()
    {
        Vector3 diffVector = drawTo.localPosition - drawFrom.localPosition;

        lineRectTransform.sizeDelta = new Vector2(diffVector.magnitude, 1f);
        lineRectTransform.pivot = new Vector2(0, 0.5f);
        lineRectTransform.position = drawFrom.position;

        float angle = Mathf.Atan2(diffVector.y, diffVector.x) * Mathf.Rad2Deg;

        lineRectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}