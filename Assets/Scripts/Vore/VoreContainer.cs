using System.Collections.Generic;
using UnityEngine;

namespace Vore
{
    public class VoreContainer : MonoBehaviour
    {
        private BasicChar[] PreyArray => GetComponentsInChildren<BasicChar>();
        private List<BasicChar> lastPreys = new List<BasicChar>();
        private bool preysDirty = true;

        public List<BasicChar> GetPreys()
        {
            if (preysDirty)
            {
                lastPreys = transform.childCount > 0 ? new List<BasicChar>(PreyArray) : new List<BasicChar>();
                preysDirty = false;
            }
            return lastPreys;
        }

        public void AddPrey(ThePrey parPrey)
        {
            preysDirty = true;
            parPrey.Prey.transform.SetParent(transform);
        }

        public void ReleasePrey(ThePrey parWho)
        {
            // Not sure if this works
            preysDirty = true;
            GameObject prey = GetPreys().Find(p => p.GetInstanceID() == parWho.Prey.GetInstanceID()).gameObject;
        }
    }
}