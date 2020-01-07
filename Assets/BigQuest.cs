using TMPro;
using UnityEngine;

public class BigQuest : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title = null, desc = null, completed = null, returnTo = null;

    public void Setup(BasicQuest basicQuest)
    {
        string str = basicQuest.Type.ToString();
        for (int i = 1; i < str.Length; i++)
        {
            char c = str[i];
            if (char.IsUpper(c))
            {
                str = str.Insert(str.IndexOf(c), " ");
            }
        }
        title.text = str;
        desc.text = QuestDesc.GetDesc(basicQuest.Type);
        string completed = $" {basicQuest.Completed}";
        if (false)
        {
            completed += $"";
        }
        string re = $"Return to:\n{1}";
    }
}