using UnityEngine;

public class EnterBuilding : MonoBehaviour
{
    [SerializeField] private CanvasMain gameUI = null;

    [SerializeField] private Building building = null;

    public GameObject BuildingToEnter => building.gameObject;

    private void Start() => gameUI = gameUI != null ? gameUI : CanvasMain.GetCanvasMain;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(PlayerHolder.GetTag))
        {
            if (building != null)
            {
                gameUI.EnterBuilding(BuildingToEnter);
            }
        }
    }
}