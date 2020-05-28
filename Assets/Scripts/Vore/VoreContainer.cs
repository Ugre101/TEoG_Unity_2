using System.Collections.Generic;
using UnityEngine;

namespace Vore
{
    public abstract class VoreContainer : MonoBehaviour
    {
        protected BasicChar[] PreyArray => GetComponentsInChildren<BasicChar>();
        protected List<BasicChar> lastPreys = new List<BasicChar>();
        protected bool preysDirty = true;

        public List<BasicChar> GetPreys()
        {
            if (preysDirty)
            {
                lastPreys = new List<BasicChar>(PreyArray);
                preysDirty = false;
            }
            return lastPreys;
        }

        public void AddPrey(ThePrey parPrey)
        {
            preysDirty = true;
            parPrey.Prey.transform.SetParent(transform);
        }

        public void ReleasePreyTo(ThePrey parWho, Transform parent, Vector3 pos)
        {
            // Not sure if this works
            preysDirty = true;
            GameObject prey = GetPreys().Find(p => p.GetInstanceID() == parWho.Prey.GetInstanceID()).gameObject;
            prey.transform.parent = parent;
            prey.transform.position = pos;
        }

        public void PreyIsdigested(ThePrey parWho)
        {
            preysDirty = true;
            GameObject prey = GetPreys().Find(p => p.GetInstanceID() == parWho.Prey.GetInstanceID()).gameObject;
            prey.transform.parent = DigestedContainer.GetContainer.transform; // Move until new game to avoid references problem
        }
    }
}