using UnityEngine;

[System.Serializable]
public class BodyStat
{
    public BodyStat(float stat)
    {
        Value = stat;
    }

    public float Value;

    public void Lose(float toLose)
    {
        Value -= toLose;
    }

    public void Gain(float toGain)
    {
        Value += toGain;
    }
}

[System.Serializable]
public class Body
{
    public Body(float h,float w,float f,float m)
    {
        height = new BodyStat(f);
        weight = new BodyStat(w);
        fat = new BodyStat(f);
        muscle = new BodyStat(m);
    }
    public BodyStat height;
    public BodyStat weight;
    public BodyStat fat;
    public BodyStat muscle;

    public string Fitness()
    {
        string a = "", b = "", c = "";
        if ((fat.Value / weight.Value) * 100f <= 2f)
        {
            a = "You look malnourished ";
        }
        else if ((fat.Value / weight.Value) * 100f <= 14f)
        {
            a = "You have an athletic body ";
        }
        else if ((fat.Value / weight.Value) * 100f <= 18f)
        {
            a = "You have a fit body ";
        }
        else if ((fat.Value / weight.Value) * 100f <= 26f)
        {
            a = "You have a healthy body ";
        }
        else if ((fat.Value / weight.Value) * 100f <= 31f)
        {
            a = "You have an pudgy body "; // Probably should change to more positive words, plus size? fat?
        }
        else if ((fat.Value / weight.Value) * 100f <= 36f)
        {
            a = "You have a plump body "; // Obese
        }
        else
        {
            a = "You have a plus size body "; // morbidly obese
        }

        if (muscle.Value < height.Value * 0.18f)
        {
            b = "with unnoticable muscle";
        }
        else if (muscle.Value < height.Value * 0.20f)
        {
            b = "with some defined muscle";
        }
        else if (muscle.Value < height.Value * 0.22f)
        {
            b = "with well-defined muscle";
        }
        else if (muscle.Value < height.Value * 0.26f)
        {
            b = "with bulky muscle";
        }
        else if (muscle.Value < height.Value * 0.30f)
        {
            b = "with hulking muscle";
        }
        else if (muscle.Value < height.Value * 0.34f)
        {
            b = "with enormous muscle";
        }
        else
        {
            b = "with colossal muscle"; // This is relative does a fairy ever have colossal muscle?
        }

        if ((fat.Value / weight.Value) * 100f <= 25f)
        {
            c = ".";
        }
        else if ((fat.Value / weight.Value) * 100f <= 31f && muscle.Value < height.Value * 0.18f)
        {
            c = " covered in fat.";
        }
        else if ((fat.Value / weight.Value) * 100f <= 38f && muscle.Value < height.Value * 0.20f)
        {
            c = " buried in fat.";
        }
        else if ((fat.Value / weight.Value) * 100f <= 55f && muscle.Value > height.Value * 0.22f)
        {
            c = "... Otherwise, you couldn't move.";
        }
        else if ((fat.Value / weight.Value) * 100f <= 55f && muscle.Value < height.Value * 0.22f)
        {
            c = "... Your weight is a burden to your ability to move.";
        }
        else
        {
            c = "... No-one knows how you move.";
        }

        return a + b + c;
    }
}