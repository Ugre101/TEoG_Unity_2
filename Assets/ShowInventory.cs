using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowInventory : MonoBehaviour
{
    public GameObject ItemPrefab;
    public BasicChar who;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        foreach (Item i in who.Inventory.Items)
        {
            GameObject shownItem = Instantiate(ItemPrefab, this.transform);
            TextMeshProUGUI[] texts = shownItem.GetComponentsInChildren<TextMeshProUGUI>();
            TextMeshProUGUI title = texts[0];
            title.text = i.Name;
        }
    }
}
