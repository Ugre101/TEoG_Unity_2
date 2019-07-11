using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Buildings : MonoBehaviour
{
    public GameObject BuildingsMain;
    public GameObject BuildingToEnter;
   private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            BuildingsMain.SetActive(true);
           foreach(Transform child in BuildingsMain.transform)
            {
                child.transform.gameObject.SetActive(false);
            }
            BuildingToEnter.SetActive(true);
        }
    }
}