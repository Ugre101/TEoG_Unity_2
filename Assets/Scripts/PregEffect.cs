using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PregEffect : BaseEffect
{
    [SerializeField] private BasicChar mother = null;
    private void Hovering()
    {
        hoverText.Hovering("Pregnant", "");
    }

    public void Setup(BasicChar parMother, GameUIHoverText hoverText)
    {
        this.hoverText = hoverText;
        mother = parMother;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
    }

    public void Status()
    {
        List<Vagina> pregVags = mother.SexualOrgans.Vaginas.FindAll(v => v.Womb.HasFetus);
        List<Fetus> fetus = pregVags.SelectMany(pv => pv.Womb.Fetuses).ToList();
        // probaly slower than foreach loop, but thats likely never relevant for perfomance
        Fetus closestToBeBorn = fetus.OrderBy(f => f.IncubationPeriod() - f.DaysOld).First();
    }
}