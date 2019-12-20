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
            Debug.Log("You forgot to assing gameUI to: " + name);
            gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<CanvasMain>();
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