using System.Collections.Generic;

namespace EssenceMenuStuff
{
    public class AddBoobs : AddOrgan
    {
        protected override Essence Ess => Player.Essence.Femi;


        protected override OrganContainer OrganContainer => Player.SexualOrgans.Boobs;

        protected override void DisplayCost() => btnText.text = $"Add boobs: {Cost}Femi";
    }
}