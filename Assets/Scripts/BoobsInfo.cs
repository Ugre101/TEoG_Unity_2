namespace SexCharStuff
{
    public class BoobsInfo : OrganInfo
    {
        public override void PrintOrganInfo() => SetText(Organs.HaveBoobs(), Organs.Boobs.Looks());
    }
}