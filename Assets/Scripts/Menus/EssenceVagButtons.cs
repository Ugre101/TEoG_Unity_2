using UnityEngine;

namespace EssenceMenu
{
    public class EssenceVagButtons : EssenceOrganButtons
    {
        [SerializeField] private AddVagina addVaginaPrefab = null;
        [SerializeField] private GrowVagina growVaginaPrefab = null;

        protected override void UpdateButtons()
        {
            transform.KillChildren();
            Instantiate(addVaginaPrefab, transform).Setup(player);
            foreach (Vagina v in player.SexualOrgans.Vaginas)
            {
                Instantiate(growVaginaPrefab, transform).Setup(player, v);
            }
            lastAmount = player.SexualOrgans.Vaginas.Count;
        }
    }
}