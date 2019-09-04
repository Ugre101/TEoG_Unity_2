using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDorm : MonoBehaviour
{
    public Dorm dorm;
    public GameObject container;
    public GameObject ServantListPrefab;
    // Start is called before the first frame update
    public void OnEnable()
    {
        ListServants();
    }
    private void ListServants()
    {
        foreach(Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(BasicChar Serv in dorm.Servants)
        {
            GameObject test = Instantiate(ServantListPrefab, container.transform);
            ShowServant showServant = test.GetComponent<ShowServant>();
            showServant.Init(Serv);
        }
    }
}
