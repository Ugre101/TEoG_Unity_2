using UnityEngine;

public class Buildings : MonoBehaviour
{
    public CanvasMain gameUI;
    public GameObject BuildingToEnter;
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