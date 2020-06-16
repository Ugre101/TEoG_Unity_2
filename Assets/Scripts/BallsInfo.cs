namespace SexCharStuff
{
    public class BallsInfo : OrganInfo
    {
        public override void PrintOrganInfo() => SetText(Organs.HaveBalls(), Organs.Balls.LooksWithOutFluids);
    }
}