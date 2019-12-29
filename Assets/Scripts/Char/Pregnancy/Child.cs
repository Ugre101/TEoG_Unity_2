using UnityEngine;

[System.Serializable]
public class Child
{
    [SerializeField]
    private float daysOld;

    [SerializeField]
    private Races race;

    [SerializeField]
    private Identity father, mother;

    public string FatherName => father.FullName;
    public string MotherName => mother.FullName;
    public Races Race => race;

    public string Age
    {
        get
        {
            if (daysOld < 30)
            {
                return $"{Mathf.FloorToInt(daysOld)} days old";
            }
            else if (daysOld < 365)
            {
                return $"{Mathf.FloorToInt(daysOld / 30)} months old";
            }
            else
            {
                return $"{Mathf.FloorToInt(daysOld / 365)} years old";
            }
        }
    }

    public Child(Races parRace, Identity parMother, Identity parFather)
    {
        race = parRace;
        father = parFather;
        mother = parMother;
    }

    public void Grow(float parDaysToGrow = 1f)
    {
        daysOld += parDaysToGrow;
        // if grow a year maybe notify?
    }
}