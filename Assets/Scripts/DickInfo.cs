namespace SexCharStuff
{
    public class DickInfo : OrganInfo
    {
        public override void PrintOrganInfo() => SetText(Organs.HaveDick(), Organs.Dicks.Looks());
    }
}