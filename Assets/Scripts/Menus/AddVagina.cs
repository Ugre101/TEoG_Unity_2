namespace EssenceMenuStuff
{
    public class AddVagina : AddOrgan
    {
        protected override Essence Ess => Player.Essence.Femi;

        protected override OrganContainer OrganContainer => Player.SexualOrgans.Vaginas;

        protected override void DisplayCost() => btnText.text = $"Add vagina: {Cost}Femi";
    }
}