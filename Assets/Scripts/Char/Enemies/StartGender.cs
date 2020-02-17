using EnemyCreatorStuff;
using UnityEngine;

[System.Serializable]
public class StartGender
{
    [SerializeField] private float amount = 100f;
    [SerializeField] private bool genderLock = false;
    [SerializeField] private Genders lockedGender = Genders.Female;
    [SerializeField] private bool favoured = false;
    [SerializeField] private GenderTypes favouredGenderType = GenderTypes.Feminine;

    public void Assing(BasicChar basicChar)
    {
        if (genderLock)
        {
            basicChar.GetEssense(amount, lockedGender);
        }
        else if (favoured)
        {
            basicChar.GetEssense(amount, favouredGenderType);
        }
        else
        {
            basicChar.GetEssense(amount);
        }
    }
}
