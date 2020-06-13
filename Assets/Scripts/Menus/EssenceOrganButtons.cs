using UnityEngine;

namespace EssenceMenuStuff
{
    public abstract class EssenceOrganButtons : MonoBehaviour
    {
        [SerializeField] protected PlayerMain player => PlayerHolder.Player;

        // Start is called before the first frame update
        protected virtual void OnEnable()
        {
            UpdateButtons();
            SexualOrgan.SomethingChanged += UpdateButtons;
        }

        protected virtual void OnDisable() => SexualOrgan.SomethingChanged -= UpdateButtons;

        protected abstract void UpdateButtons();
    }
}