using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryHandler : MonoBehaviour
{
    public GameObject ItemPrefab;
    public playerMain player;
    public GameObject ItemContainer;
    public List<GameObject> Items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        foreach(Transform child in ItemContainer.transform)
        {
            Destroy(child.transform.gameObject);
        }
        foreach(Item item in player.Inventory.Items)
        {
            Instantiate(TheItem(item), ItemContainer.transform);
        }
    }
    private GameObject TheItem(Item item)
    {
        GameObject ItemObject = ItemPrefab;
        TextMeshProUGUI[] texts = ItemObject.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI title = texts[0];
        title.text = item.Title;
        TextMeshProUGUI useText = texts[1];
        useText.text = item.UseName;
        Button[] buttons = ItemObject.GetComponentsInChildren<Button>();
        Debug.Log(buttons.Length);
        Button Use = buttons[0];
        return ItemObject;
    }
}
