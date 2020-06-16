using UnityEngine;

namespace EssenceMenuStuff
{
    public class EssenceVagButtons : EssenceOrganButtons
    {
        [SerializeField] private AddVagina addVaginaPrefab = null;
        [SerializeField] private GrowVagina growVaginaPrefab = null;

        protected override void UpdateButtons()
        {
            transform.KillChildren();
            Instantiate(addVaginaPrefab, transform).Setup().onClick.AddListener(UpdateButtons);
            foreach (Vagina v in player.SexualOrgans.Vaginas.List)
            {
                Instantiate(growVaginaPrefab, transform).Setup(player, v);
            }
        }
    }
}