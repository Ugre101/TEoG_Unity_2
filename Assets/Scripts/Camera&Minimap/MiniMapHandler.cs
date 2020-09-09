using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapHandler : MonoBehaviour
{
    public GameObject miniMap, bigMap;
    private bool mini = true, big = false;
    // Start is called before the first frame update
    private void Start()
    {
        if (miniMap == null || bigMap == null)
        {
            // if map is missing deativate code to avoid errors.
            this.enabled = false;
        }
        miniMap.SetActive(mini);
        bigMap.SetActive(big);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UpdateMaps();
        }
    }
    private void UpdateMaps()
    {
        mini = !mini;
        big = !big;
        miniMap.SetActive(mini);
        bigMap.SetActive(big);
    }
}
