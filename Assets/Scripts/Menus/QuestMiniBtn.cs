using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestMiniBtn : MonoBehaviour
{
    [SerializeField]
    private Image icon = null;

    [SerializeField]
    private TextMeshProUGUI title = null;

    [SerializeField]
    private TextMeshProUGUI desc = null;

    [SerializeField]
    private Button btn;

    private BasicQuest quest;
    private TextMeshProUGUI bigText;

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        btn.onClick.AddListener(ToBigText);
    }

    public void Init(BasicQuest toAdd, TextMeshProUGUI big)
    {
        quest = toAdd;
        bigText = big;
        string str = quest.Type.ToString();
        for (int i = 1; i < str.Length; i++)
        {
            char c = str[i];
            if (char.IsUpper(c))
            {
                str = str.Insert(str.IndexOf(c), " ");
            }
        }
        title.text = str;
        desc.text = $"Completed: {quest.Completed}";
        icon.sprite = null;
    }

    public void ToBigText()
    {
        // bigText.text = quest.Title;
    }
}