using UnityEngine;

public class HomeMain : MonoBehaviour
{
    public CanvasMain gameUI;
    public GameObject HouseStart;
    public Home home;

    public void ToStart()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        HouseStart.SetActive(true);
    }
}