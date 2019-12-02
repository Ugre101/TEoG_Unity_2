using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMenuHandler : MonoBehaviour
{
    [Header("Quest prefab")]
    public GameObject Prefab;
    public PlayerMain player;
    public TextMeshProUGUI bigQuestText;
    private string nl = Environment.NewLine;

    //  private Quest last;
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
        QuestMiniBtn miniQuest = AQuest.GetComponent<QuestMiniBtn>();
        miniQuest.Init(q, bigQuestText);
     /*   TextMeshProUGUI[] texts = AQuest.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI title = texts[0];
        TextMeshProUGUI info = texts[1];
        title.text = $"{q.Title}";
        info.text = $"Completed: {q.Completed}{nl}Count: {q.Count}{nl}"; 
        if (q.HasTiers)
        {
         //   q.HasTiers ? $"Tier: {q.Tier}" : "";
        }
        Image icon = AQuest.gameObject.transform.GetChild(3).GetComponent<Image>();
        icon.sprite = null; */
    }
}
