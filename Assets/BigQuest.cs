using System.Text;
using TMPro;
using UnityEngine;

public class BigQuest : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title = null, desc = null, completed = null, returnTo = null;

    public void Setup(BasicQuest basicQuest)
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
}