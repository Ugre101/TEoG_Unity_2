namespace EssenceMenuStuff
{
    public class AddDick : AddOrgan
    {
        protected override Essence Ess => Player.Essence.Masc;


        protected override OrganContainer OrganContainer => Player.SexualOrgans.Dicks;

        protected override void DisplayCost() => btnText.text = $"Add dick: {Cost}";
    }
}