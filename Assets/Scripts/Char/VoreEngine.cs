using System.Collections.Generic;
using UnityEngine;

public abstract class VoreEngine : MonoBehaviour
{
    [SerializeField]
    private List<BasicChar> stomach = new List<BasicChar>();

    public List<BasicChar> Stomach { get { return stomach; } }
    private float StomachCap;
    private float StomachCur;
    private int StomachExp;
    public void StomachVore(BasicChar prey,BasicChar pred)
    {
        // if pred capacity > prey weight eat prey.
        stomach.Add(prey);
    }

}
public class VoreBalls
{
    private List<BasicChar> preys = new List<BasicChar>();
    public List<BasicChar> Preys = new List<BasicChar>();
    
    public float Cap(List<Balls> balls)
    {
        float cap = 0;
        foreach(Balls b in balls)
        {
            cap += b.Size;
        }
        return cap;
    }

}
public class Prey
{
    public BasicChar prey;
    private float startWeight;
    public float StartWeight { get { return startWeight; } }
}
