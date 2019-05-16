using System.Collections.Generic;
using UnityEngine;

public abstract class VoreEngine : MonoBehaviour
{
    [SerializeField]
    private List<BasicChar> stomach = new List<BasicChar>();

    public List<BasicChar> Stomach { get { return stomach; } }

    public void StomachVore(BasicChar prey,BasicChar pred)
    {
        // if pred capacity > prey weight eat prey.
        stomach.Add(prey);
    }

    // Start is called before the first frame update
    private void Start()
    {
        // every something digest prey
        //InvokeRepeating()
    }

    // Update is called once per frame
    private void Update()
    {
    }
}