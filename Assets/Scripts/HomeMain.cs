using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMain : MonoBehaviour
{
    public GameUI gameUI;
    public GameObject HouseStart;
    public void ToStart()
    {
        foreach( Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        HouseStart.SetActive(true);
    }
}
