using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class DickContainer : OrganContainer
{
    [SerializeField] private List<Dick> dicks = new List<Dick>();
    public List<Dick> List => dicks;

    public override string Looks
    {
        get
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < List.Count; i++)
            {
                Dick dick = List[i];
                builder.Append(i == 0 ? dick.Look() : dick.Look(false));
                if (i == List.Count - 2)
                {
                    builder.Append(" and ");
                }
                else if (i == List.Count - 1)
                {
                    builder.Append(".");
                }
                else
                {
                    builder.Append(", ");
                }
            }
            return builder.ToString();
        }
    }

    public override float AddCost => Mathf.Round(30 * Mathf.Pow(4, List.Count));

    public override float BiggestSizeValue => List.Select(so => so.Size).DefaultIfEmpty(0).Max();

    public override void AddNew(int baseSize)
    {
        List.Add(new Dick(baseSize));
        InvokeOrganChange();
    }

    public override void AddNew()
    {
        List.Add(new Dick());
        InvokeOrganChange();
    }

    public override void ReBind()
    {
        List.ForEach(o => o.SomethingChanged -= InvokeOrganChange);
        List.ForEach(o => o.SomethingChanged += InvokeOrganChange);
    }

    public override float ReCycle()
    {
        Dick toShrink = List[List.Count - 1];
        if (toShrink.Shrink())
        {
            Remove(toShrink);
            return 30f;
        }
        else
        {
            return toShrink.Cost;
        }
    }

    public void Remove(Dick toRemove)
    {
        List.Remove(toRemove);
        InvokeOrganChange();
    }
}
