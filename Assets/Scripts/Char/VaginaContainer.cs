using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class VaginaContainer : OrganContainer
{
    [SerializeField] private List<Vagina> vaginas = new List<Vagina>();
    public List<Vagina> List => vaginas;

    public override string Looks
    {
        get
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < List.Count; i++)
            {
                Vagina vag = List[i];
                builder.Append(i == 0 ? vag.Look() : vag.Look(false));
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

    public override void AddNew()
    {
        List.Add(new Vagina());
        InvokeOrganChange();
    }

    public override void AddNew(int baseSize = 1)
    {
        List.Add(new Vagina(baseSize));
        InvokeOrganChange();
    }

    public override void ReBind()
    {
        List.ForEach(o => o.SomethingChanged -= InvokeOrganChange);
        List.ForEach(o => o.SomethingChanged += InvokeOrganChange);
    }

    public override float ReCycle()
    {
        Vagina toShrink = List[vaginas.Count - 1];
        if (!toShrink.Shrink()) return toShrink.Cost;
        
        Remove(toShrink);
        return 30f;
    }

    public void Remove(Vagina toRemove)
    {
        List.Remove(toRemove);
        InvokeOrganChange();
    }
}