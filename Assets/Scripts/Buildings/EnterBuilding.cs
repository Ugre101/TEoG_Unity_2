using UnityEngine;

public class EnterBuilding : MonoBehaviour
{
    [SerializeField]
    private CanvasMain gameUI = null;

    [SerializeField]
    private Building building = null;

    public GameObject BuildingToEnter => building.gameObject;

    private void Start()
    {
        // incase of forgetting to assing gameui for new building
        if (gameUI == null)
        {
            gameUI = CanvasMain.GetCanvasMain;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            gameUI.EnterBuilding(BuildingToEnter);
        }
    }
}