using UnityEngine;

namespace EssenceMenuStuff
{
    public abstract class EssenceOrganButtons : MonoBehaviour
    {
        protected BasicChar Player => PlayerMain.Player;

        // Start is called before the first frame update
        protected virtual void OnEnable()
        {
            UpdateButtons();
            Player.SexualOrgans.AllOrgans.ForEach(o => o.Change += UpdateButtons);
        }

        protected virtual void OnDisable() => Player.SexualOrgans.AllOrgans.ForEach(o => o.Change -= UpdateButtons);

        protected abstract void UpdateButtons();
    }
}