using UnityEngine;
using UnityEngine.UI;

public class SmarterGrid : LayoutGroup
{
    [SerializeField] private int rows = 0, collums = 0;
    [SerializeField] private Vector2 cellSize = new Vector2(0, 0), spacing = new Vector2(5, 5);

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float sqrRt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrRt);
        collums = Mathf.CeilToInt(sqrRt);

        float parentWidth = rectTransform.rect.width,
            parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / collums) - (spacing.x / collums * (collums - 1)),
            cellHeight = (parentHeight / rows) - (spacing.y / rows * (rows - 1));
       
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