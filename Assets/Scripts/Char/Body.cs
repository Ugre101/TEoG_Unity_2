using UnityEngine;

[System.Serializable]
public class BodyStat
{
    public BodyStat(float stat) => value = stat;

    [SerializeField]
    private float value;

    public float Value => value;

    /// <summary>Max(Value - Abs(toLose),0.01f)</summary>
    public void Lose(float toLose) => value = Mathf.Max(value - Mathf.Abs(toLose), 0.01f);

    /// <summary>Value += Abs(toGain)</summary>
    public void Gain(float toGain) => value += Mathf.Abs(toGain);
}

[System.Serializable]
public class Body
{
    public Body(float parHeight, float parFat, float parMuscle)
    {
        height = new BodyStat(parHeight);
        fat = new BodyStat(parFat);
        muscle = new BodyStat(parMuscle);
    }

    [SerializeField]
    private BodyStat height, fat, muscle;

    public BodyStat Height => height;
    public BodyStat Fat => fat;
    public BodyStat Muscle => muscle;

    // TODO centaurs and etc need to weight more and in future maybe add diffrent settings for female body frame

    ///<summary> Bones and organs = Height * 0.15; add to that weight of Fat and Muscle </summary>
    public float Weight => Height.Value * 0.15f + Fat.Value + Muscle.Value;

    ///<summary>Body fat percentage</summary>
    public float FatPer => Fat.Value / Weight * 100f;

    private bool FatPerLowerThan(float parPer) => FatPer <= parPer;

    private bool MuscleLessHeight(float f) => Muscle.Value < Height.Value * f;

    private bool MuscleMoreHeight(float f) => Muscle.Value > Height.Value * f;

    public string Fitness()
    {
        string a = FatPerLowerThan(2f) ? "You look malnourished " :
        FatPerLowerThan(14f) ? "You have an athletic body " :
        FatPerLowerThan(18f) ? "You have a fit body " :
        FatPerLowerThan(26f) ? "You have a healthy body " :
        FatPerLowerThan(31f) ? "You have an pudgy body " :
        FatPerLowerThan(36f) ? "You have a plump body " :
        "You have a plus size body ";  // morbidly obese

        string b = MuscleLessHeight(0.18f) ? "with unnoticable muscle" :
        MuscleLessHeight(0.20f) ? "with some defined muscle" :
        MuscleLessHeight(0.22f) ? "with well-defined muscle" :
        MuscleLessHeight(0.26f) ? "with bulky muscle" :
        MuscleLessHeight(0.30f) ? "with hulking muscle" :
        MuscleLessHeight(0.34f) ? "with enormous muscle" :
        "with colossal muscle"; // This is relative does a fairy ever have colossal muscle?

        string c = FatPerLowerThan(25f) ? "." :
        FatPerLowerThan(31f) && MuscleLessHeight(0.18f) ? " covered in fat." :
        FatPerLowerThan(38f) && MuscleLessHeight(0.20f) ? " buried in fat." :
        FatPerLowerThan(55f) && MuscleMoreHeight(0.22f) ? "... Otherwise, you couldn't move." :
        FatPerLowerThan(55f) && MuscleLessHeight(0.22f) ? "... Your weight is a burden to your ability to move." :
         "... No-one knows how you move.";

        return a + b + c;
    }
}