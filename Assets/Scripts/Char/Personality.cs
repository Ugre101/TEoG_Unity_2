using UnityEngine;

public enum SubDom
{
    // TODO add more and improve
    Submissive,

    Neutral,
    Dominant,
}

[System.Serializable]
public class Personality
{
    [SerializeField] private int value = 0;

    public SubDom Current
    {
        get
        {
            if (value < -100)
            {
                return SubDom.Submissive;
            }
            else if (value > -100 && value < 100)
            {
                return SubDom.Neutral;
            }
            else
            {
                return SubDom.Dominant;
            }
        }
    }

    public void TurnSub(int gain = 1) => value -= gain;

    public void TurnDom(int gain = 1) => value += gain;
}