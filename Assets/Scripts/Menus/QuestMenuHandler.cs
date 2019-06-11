using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMenuHandler : MonoBehaviour
{
    [Header("Quest prefab")]
    public GameObject Prefab;
    public playerMain player;
    private string nl = Environment.NewLine;

    private void OnEnable()
    {
        foreach(Transform child in this.transform)
        {
           GameObject.Destroy(child.gameObject);
        }
        foreach(Quest q in player.Quest.List)
        {
            QuestPrefab(q);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void QuestPrefab(Quest q)
    {
        GameObject AQuest = Instantiate(Prefab, this.transform);
        TextMeshProUGUI[] texts = AQuest.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI title = texts[0];
        TextMeshProUGUI info = texts[1];
        title.text = $"{q.Title}";
        info.text = string.Format("Completed: {0}{1}Count: {2}{1}{3}", q.Completed,nl,q.Count,q.HasTiers ? $"Tier: {q.Tier}" : "");
        Image icon = AQuest.gameObject.transform.GetChild(3).GetComponent<Image>();
        icon.sprite = null;
    }
}
