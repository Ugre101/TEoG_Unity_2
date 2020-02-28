namespace SexCharStuff
{
    public class BallsInfo : OrganInfo
    {
        public override void PrintOrganInfo() => SetText(Organs.HaveBalls(), detailed ? Organs.Balls.Looks() : Organs.Balls.Looks(false));
    }
}