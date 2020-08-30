namespace EssenceMenuStuff
{
    public class AddBalls : AddOrgan
    {
        //        private List<Balls> Balls => Player.SexualOrgans.Balls;

        protected override Essence Ess => Player.Essence.Masc;

        protected override OrganContainer OrganContainer => Player.SexualOrgans.Balls;

        protected override void DisplayCost() => btnText.text = $"Add balls: {Cost}";
    }
}