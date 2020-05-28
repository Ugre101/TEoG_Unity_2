using TMPro;
using UnityEngine;

public class DialogChar : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI raceGender, nameTitle;
    protected BasicChar whom;

    public void Setup(BasicChar whom)
    {
        this.whom = whom;
        SetTexts(whom);
    }

    protected void SetTexts(BasicChar whom)
    {
        raceGender.text = $"{whom.Race(true)}\n{whom.GetGender()}";
        nameTitle.text = whom.Identity.FullName;
    }
}
