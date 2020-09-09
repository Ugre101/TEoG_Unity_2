using System.Text;
using TMPro;
using UnityEngine;

public class BigQuest : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title = null, desc = null, completed = null, returnTo = null;

    private BasicQuest basicQuest;

    public void Setup(BasicQuest basicQuest)
    {
        this.basicQuest = basicQuest;
        PrintQuest(basicQuest);
    }

    private void OnEnable()
    {
        if (basicQuest != null)
        {
            if (QuestsSystem.HasQuest(basicQuest.Type))
            {
                PrintQuest(basicQuest);
            }
            else
            {
                basicQuest = null;
                ClearQuest();
            }
        }
    }

    private void ClearQuest()
    {
        title.text = string.Empty;
        desc.text = string.Empty;
        completed.text = string.Empty;
        returnTo.text = string.Empty;
    }

    private void PrintQuest(BasicQuest basicQuest)
    {
        string str = SpaceAtUpper(basicQuest.Type.ToString());

        title.text = str;
        desc.text = QuestDesc.GetDesc(basicQuest.Type);
        string isCompleted = $"Completed: {basicQuest.Completed}";
        if (basicQuest is TieredQuest tiered)
        {
            isCompleted += $"\nTier: {tiered.Tier}";
        }
        completed.text = isCompleted;
        returnTo.text = $"Return to:\n{QuestDesc.QuestReturnTo(basicQuest.Type)}";
    }

    private static string SpaceAtUpper(string text)
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
}