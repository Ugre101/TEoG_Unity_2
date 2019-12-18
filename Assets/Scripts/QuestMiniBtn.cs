using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestMiniBtn : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;
    private Button btn;
    private BasicQuest quest;
    private TextMeshProUGUI bigText;

    // Start is called before the first frame update
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToBigText);
    }

    public void Init(BasicQuest toAdd, TextMeshProUGUI big)
    {
        quest = toAdd;
        bigText = big;
      //  title.text = quest.Title;
        desc.text = $"Completed: {quest.Completed}";
        icon.sprite = null;
    }

    public void ToBigText()
    {
       // bigText.text = quest.Title;
    }
}