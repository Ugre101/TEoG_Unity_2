using System.Collections.Generic;
using UnityEngine;

public class SmartTreeObjectPool : MonoBehaviour
{
    public static SmartTreeObjectPool Instance { get; private set; }

    [SerializeField] private SmartTree smartTreePrefab = null;
    private Queue<SmartTree> inActiveTrees;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Grab editor added trees
            inActiveTrees = new Queue<SmartTree>(GetComponentsInChildren<SmartTree>());
        }
        else
            Destroy(gameObject);
    }

    public SmartTree GetTree() => inActiveTrees.Count > 0 ? inActiveTrees.Dequeue() : Instantiate(smartTreePrefab);

    public void ReturnTree(SmartTree smartTree)
    {
        smartTree.gameObject.SetActive(false);
        smartTree.transform.SetParent(transform);
        inActiveTrees.Enqueue(smartTree);
    }
}