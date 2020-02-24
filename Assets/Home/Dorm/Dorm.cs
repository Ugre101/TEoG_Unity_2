using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dorm : MonoBehaviour
{
    public static Dorm GetDrom { get; private set; }
    [SerializeField] private List<BasicChar> servantPrefabs = new List<BasicChar>();
    [SerializeField] private BasicChar defaultPrefab = null;
    public bool HasSpace => StartHomeStats.Dorm.Level * 3 > transform.childCount;

    private BasicChar[] ArrayServants => GetComponentsInChildren<BasicChar>();
    private bool ServantsDirty = true;
    private List<BasicChar> lastServants = new List<BasicChar>();

    public List<BasicChar> Servants
    {
        get
        {
            if (ServantsDirty)
            {
                lastServants = new List<BasicChar>(ArrayServants);
                ServantsDirty = false;
            }
            return lastServants;
        }
    }

    private void Awake()
    {
        if (GetDrom == null)
        {
            GetDrom = this;
        }
        else if (GetDrom != this)
        {
            Destroy(gameObject);
        }
    }

    public BasicChar AddTo(BasicChar toAdd)
    {
        BasicChar sercv = Instantiate(toAdd, transform);
        sercv.name = toAdd.name;
        ServantsDirty = true;
        return sercv;
    }

    public void MoveToDorm(BasicChar toMove)
    {
        toMove.transform.SetParent(this.transform);
        toMove.transform.position = transform.position;
    }

    public List<DormSave> Save()
    {
        List<DormSave> dormSaves = new List<DormSave>();
        foreach (Transform child in transform)
        {
            DormSave tempDorm = new DormSave(child.name, child.GetComponent<BasicChar>());
            dormSaves.Add(tempDorm);
        }
        return dormSaves;
    }

    public void Load(List<DormSave> toLoad)
    {
        transform.KillChildren();
        foreach (DormSave ds in toLoad)
        {
            if (servantPrefabs.Exists(n => n.name == ds.Name))
            {
                BasicChar loaded = AddTo(servantPrefabs.Find(n => n.name == ds.Name));
                StartCoroutine(WaitAFrame(ds, loaded));
            }
            else
            {
                BasicChar loaded = AddTo(defaultPrefab);
                StartCoroutine(WaitAFrame(ds, loaded));
            }
        }
    }

    private IEnumerator WaitAFrame(DormSave dormSave, BasicChar basicChar)
    {
        // wait a frame to let new basicchar to fully load before overwritting it
        // todo maybe make it so a list of char so we don't have to wait a frame for every char; could take some time.
        yield return new WaitForEndOfFrame();
        JsonUtility.FromJsonOverwrite(dormSave.Who, basicChar);
    }
}

[System.Serializable]
public struct DormSave
{
    [SerializeField] private string name;
    [SerializeField] private string who;

    public string Name => name;
    public string Who => who;

    public DormSave(string Name, BasicChar Who)
    {
        name = Name;
        who = JsonUtility.ToJson(Who);
    }
}