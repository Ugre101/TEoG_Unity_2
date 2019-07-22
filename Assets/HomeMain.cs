using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMain : MonoBehaviour
{
    public GameObject HouseStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnterHome()
    {
        this.transform.gameObject.SetActive(true);
        foreach(Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        HouseStart.SetActive(true);
    }
}
