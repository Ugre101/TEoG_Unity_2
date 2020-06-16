namespace SexCharStuff
{
    public class VaginaInfo : OrganInfo
    {
        public override void PrintOrganInfo() => SetText(Organs.HaveVagina(), Organs.Vaginas.Looks);
    }
}