using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryHandler : MonoBehaviour
{
    public GameObject ItemPrefab;
    public playerMain player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject TheItem(Item item)
    {
        GameObject ItemObject = ItemPrefab;
        TextMeshProUGUI[] texts = ItemObject.GetComponentsInChildren<TextMeshProUGUI>();
        return ItemObject;
    }
}
