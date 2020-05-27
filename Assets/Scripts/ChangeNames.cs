using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNames : MonoBehaviour
{
    [SerializeField] private ChangeNamesPerson person = null;
    [SerializeField] private Transform container = null;
    [SerializeField] private Button doneBtn = null;

    public ChangeNames Setup(List<Identity> identities)
    {
        container.KillChildren();
        for (int i = 0; i < identities.Count; i++)
        {
            Identity identity = (Identity)identities[i];
            Instantiate(person, container).Setup(i, identity);
        }
        doneBtn.onClick.AddListener(Click);
        return this;
    }

    public ChangeNames Setup(List<Identity> identities, string lastName)
    {
        container.KillChildren();
        for (int i = 0; i < identities.Count; i++)
        {
            Identity identity = (Identity)identities[i];
            Instantiate(person, container).Setup(i, identity);
        }
        doneBtn.onClick.AddListener(Click);
        return this;
    }

    private void Click()
    {
        Done?.Invoke();
        Destroy(gameObject);
    }

    public delegate void IsDone();

    public event IsDone Done;
}