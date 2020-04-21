using UnityEngine;

namespace Vore
{
    public class VoreVaginaContainer : VoreContainer
    {
        public void PreyIsRebithed(ThePrey parWho)
        {
            preysDirty = true;
            GameObject prey = GetPreys().Find(p => p.GetInstanceID() == parWho.Prey.GetInstanceID()).gameObject;
            prey.transform.parent = DigestedContainer.GetContainer.transform;
            // TODO maybe change?
        }
    }
}