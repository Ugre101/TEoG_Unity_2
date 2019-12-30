using UnityEngine;

[System.Serializable]
public class Essence
{
    [SerializeField]
    protected float _amount;

    public float Amount => Mathf.Floor(_amount);
    public string StringAmount => _amount > 999 ? Mathf.Round(_amount / 1000) + "k" : _amount.ToString();

    public Essence() => _amount = 0;

    public Essence(float parAmount) => _amount = parAmount;

    public void Gain(float toGain)
    {
        _amount += Mathf.Max(0, toGain);
        EssenceSliderEvent?.Invoke();
    }

    public float Lose(float toLose)
    {
        float lose = Mathf.Min(_amount, toLose);
        _amount -= lose;
        EssenceSliderEvent?.Invoke();
        return lose;
    }

    public void ManualUpdate() => EssenceSliderEvent?.Invoke();

    public delegate void EssenceSlider();

    public event EssenceSlider EssenceSliderEvent;
}
public static class EssenceExtension
{

    public static float LoseMasc(this BasicChar who, float mascToLose)
    {
        float have = who.Masc.Lose(mascToLose);
        float missing = mascToLose - have;
        if (missing > 0)
        {
            float fromOrgans = 0f;
            while (missing > fromOrgans && (who.SexualOrgans.Dicks.Count > 0 || who.SexualOrgans.Balls.Count > 0))// have needed organs
            {
                if (who.SexualOrgans.Balls.Count > 0 && who.SexualOrgans.Dicks.Count > 0)
                {
                    if (who.SexualOrgans.Dicks.Total() >= who.SexualOrgans.Balls.Total() * 2f + 1f)
                    {
                        fromOrgans += who.SexualOrgans.Dicks.ReCycle();
                    }
                    else
                    {
                        fromOrgans += who.SexualOrgans.Balls.ReCycle();
                    }
                }
                else if (who.SexualOrgans.Balls.Count > 0)
                {
                    fromOrgans += who.SexualOrgans.Balls.ReCycle();
                }
                else
                {
                    fromOrgans += who.SexualOrgans.Dicks.ReCycle();
                }
            }
            have += Mathf.Min(fromOrgans, missing);
            float left = fromOrgans - missing;
            if (left > 0)
            {
                who.Masc.Gain(left);
            }
        }
        return have;
    }

    public static float LoseFemi(this BasicChar who, float femiToLose)
    {
        float have = who.Femi.Lose(femiToLose);
        float missing = femiToLose - have;
        if (missing > 0)
        {
            float fromOrgans = 0f;
            while (missing > fromOrgans && (who.SexualOrgans.Vaginas.Count > 0 || who.SexualOrgans.Boobs.Count > 0))// have needed organs
            {
                if (who.SexualOrgans.Boobs.Count > 0 && who.SexualOrgans.Vaginas.Count > 0)
                {
                    if (who.SexualOrgans.Boobs.Total() >= who.SexualOrgans.Vaginas.Total() * 2f + 1f)
                    {
                        fromOrgans += who.SexualOrgans.Boobs.ReCycle();
                    }
                    else
                    {
                        fromOrgans += who.SexualOrgans.Vaginas.ReCycle();
                    }
                }
                else if (who.SexualOrgans.Vaginas.Count > 0)
                {
                    fromOrgans += who.SexualOrgans.Vaginas.ReCycle();
                }
                else
                {
                    fromOrgans += who.SexualOrgans.Boobs.ReCycle();
                }
            }
            have += Mathf.Min(fromOrgans, missing);
            float left = fromOrgans - missing;
            if (left > 0)
            {
                who.Femi.Gain(left);
            }
        }
        return have;
    }
}