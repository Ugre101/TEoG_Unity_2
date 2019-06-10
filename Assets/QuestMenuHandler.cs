using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestMenuHandler : MonoBehaviour
{
    [Header("Quest prefab")]
    public GameObject Prefab;
    public playerMain player;
    // Start is called before the first frame update
    void Start()
    {
        QuestPrefab();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void QuestPrefab()
    {
        GameObject AQuest = Instantiate(Prefab, this.transform);
        TextMeshProUGUI[] texts = AQuest.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI title = texts[0];
        TextMeshProUGUI info = texts[1];
        title.text = "Bandit";
        Image icon = AQuest.gameObject.transform.GetChild(3).GetComponent<Image>();
        icon.sprite = null;
    }
}
