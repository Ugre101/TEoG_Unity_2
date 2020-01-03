using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PregEffect : BaseEffect
{
    [SerializeField]
    private BasicChar mother = null;

    public void Setup(BasicChar parMother)
    {
        mother = parMother;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }

    public void Status()
    {
        List<Vagina> pregVags = mother.SexualOrgans.Vaginas.FindAll(v => v.Womb.HasFetus);
        List<Fetus> fetus = pregVags.SelectMany(pv => pv.Womb.Fetuses).ToList();
        // probaly slower than foreach loop, but thats likely never relevant for perfomance
        Fetus closestToBeBorn = fetus.OrderBy(f => f.IncubationPeriod() - f.DaysOld).First();
    }
}