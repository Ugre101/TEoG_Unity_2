using UnityEngine;
using UnityEngine.UI;

public class SmarterGrid : LayoutGroup
{
    [SerializeField] private int rows = 0, collums = 0;
    [SerializeField] private Vector2 cellSize = new Vector2(0, 0), spacing = new Vector2(5, 5);
    [SerializeField] private bool hasMinSize = false, hasMaxSize = false;
    [SerializeField] private Vector2 minSize = new Vector2(), maxSize = new Vector2();

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float sqrRt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrRt);
        collums = Mathf.CeilToInt(sqrRt);

        float parentWidth = rectTransform.rect.width,
            parentHeight = rectTransform.rect.height;

        CellSizes(parentWidth, parentHeight, out float cellWidth, out float cellHeight);

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            int rowCount = i / collums;
            int collumnCount = i % collums;
            var item = rectChildren[i];

            var xPos = (cellSize.x * collumnCount) + (spacing.x * collumnCount);
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount);

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    private void CellSizes(float parentWidth, float parentHeight, out float cellWidth, out float cellHeight)
    {
        if (hasMinSize && hasMaxSize)
        {
            cellWidth = Mathf.Clamp((parentWidth / collums) - (spacing.x / collums * (collums - 1)), minSize.x, maxSize.x);
            cellHeight = Mathf.Clamp((parentHeight / rows) - (spacing.y / rows * (rows - 1)), minSize.y, maxSize.y);
        }
        else if (hasMinSize)
        {
            cellWidth = Mathf.Max((parentWidth / collums) - (spacing.x / collums * (collums - 1)), minSize.x);
            cellHeight = Mathf.Max((parentHeight / rows) - (spacing.y / rows * (rows - 1)), minSize.y);
        }
        else if (hasMaxSize)
        {
            cellWidth = Mathf.Min((parentWidth / collums) - (spacing.x / collums * (collums - 1)), maxSize.x);
            cellHeight = Mathf.Min((parentHeight / rows) - (spacing.y / rows * (rows - 1)), maxSize.y);
        }
        else
        {
            cellWidth = (parentWidth / collums) - (spacing.x / collums * (collums - 1));
            cellHeight = (parentHeight / rows) - (spacing.y / rows * (rows - 1));
        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}