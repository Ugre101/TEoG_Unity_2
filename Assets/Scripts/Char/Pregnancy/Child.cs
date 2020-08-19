using UnityEngine;

[System.Serializable]
public class Child
{
    [SerializeField] private float daysOld;

    [SerializeField] private Races race;

    [SerializeField] private Identity child, father, mother;
    [SerializeField] private bool playerMother, playerFather;
    public Identity Father => father;
    public Identity Mother => mother;
    public Identity ChildIdentity => child;
    public string ChildName => child.FullName;
    public string FatherName => Father.FullName;
    public string MotherName => Mother.FullName;
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

    public bool PlayerMother => playerMother;
    public bool PlayerFather => playerFather;

    public Child(Races parRace, Identity parMother) : this(parRace, parMother, new Identity()) // Single mother
    {
    }

    public Child(Races parRace, Identity parMother, Identity parFather, bool playerFather = false, bool playerMother = false)
    {
        race = parRace;
        child = new Identity();
        father = parFather;
        mother = parMother;
        this.playerMother = playerMother;
        this.playerFather = playerFather;
    }

    public void Grow(float parDaysToGrow = 1f)
    {
        daysOld += parDaysToGrow;
        // if grow a year maybe notify?
    }
}