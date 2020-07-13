using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PregEffect : BaseEffect
{
    [SerializeField] private BasicChar mother = null;

    protected override void Hovering() => hoverText.Hovering("Pregnant", "");

    public void Setup(BasicChar parMother, GameUIHoverText hoverText)
    {
        this.hoverText = hoverText;
        mother = parMother;
        DateSystem.NewDayEvent += Status;
    }

    private void OnDestroy() => DateSystem.NewDayEvent -= Status;

    public void Status()
    {
        List<Vagina> pregVags = mother.SexualOrgans.Vaginas.List.FindAll(v => v.Womb.HasFetus);
        List<Fetus> fetus = pregVags.SelectMany(pv => pv.Womb.Fetuses).ToList();
        // probaly slower than foreach loop, but thats likely never relevant for perfomance
        Fetus closestToBeBorn = fetus.OrderBy(f => f.IncubationPeriod() - f.DaysOld).First();
    }
}