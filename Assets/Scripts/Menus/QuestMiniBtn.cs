using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestMiniBtn : MonoBehaviour
{
    [SerializeField] private Image icon = null;

    [SerializeField] private TextMeshProUGUI title = null, desc = null, tier = null;

    [SerializeField] private Button btn;

    private BasicQuest quest;
    private BigQuest bigQuest;

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        btn.onClick.AddListener(ToBigText);
    }

    public void Init(BasicQuest toAdd, BigQuest big)
    {
        quest = toAdd;
        bigQuest = big;
        string str = SpaceAtUpper(quest.Type.ToString());
        title.text = str;
        desc.text = $"Completed: {quest.Completed}";
        if (quest is TieredQuest tiered)
        {
            tier.text = $"Tier: {tiered.Tier}";
        }
        else
        {
            tier.gameObject.SetActive(false);
        }
        icon.sprite = null;
    }

    private string SpaceAtUpper(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            StringBuilder stringBuilder = new StringBuilder(text.Length * 2);
            stringBuilder.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsUpper(c) && c != ' ')
                {
                    stringBuilder.Append(' ');
                }
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }
        return string.Empty;
    }

    public void ToBigText() => bigQuest.Setup(quest);
}