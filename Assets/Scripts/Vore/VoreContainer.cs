using System.Collections.Generic;
using UnityEngine;

namespace Vore
{
    public abstract class VoreContainer : MonoBehaviour
    {
        private ThePrey[] GetPreys => GetComponentsInChildren<ThePrey>();
        private List<ThePrey> lastPreys = new List<ThePrey>();
        private bool preysDirty = true;

        public List<ThePrey> Preys
        {
            get
            {
                if (preysDirty)
                {
                    lastPreys = new List<ThePrey>(GetPreys);
                    preysDirty = false;
                }
                return lastPreys;
            }
        }

        public void AddPrey(global::ThePrey parPrey)
        {
            preysDirty = true;
            parPrey.transform.SetParent(transform);
        }

        public void ReleasePrey(ThePrey parWho)
        {
            // Not sure if this works
            preysDirty = true;
            GameObject prey = Preys.Find(p => p.Prey.GetInstanceID() == parWho.Prey.GetInstanceID()).Prey.gameObject;
        }
    }
}