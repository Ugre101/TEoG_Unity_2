using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dorm : MonoBehaviour
{
    [SerializeField]
    private Home home = null;

    public bool HasSpace => home.Stats.Dorm.Level * 3 > transform.childCount;

    private BasicChar[] ArrayServants => GetComponentsInChildren<BasicChar>();
    private bool ServantsDirty = true;
    private List<BasicChar> lastServants = null;

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

    public void AddTo(GameObject toAdd)
    {
        GameObject sercv = Instantiate(toAdd, transform);
        sercv.name = toAdd.name;
        ServantsDirty = true;
    }

    [SerializeField]
    private List<GameObject> servantPrefabs = new List<GameObject>();

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
            if (servantPrefabs.Exists(n => n.name == ds.name))
            {
                GameObject loaded = Instantiate(servantPrefabs.Find(n => n.name == ds.name), transform);
                loaded.name = ds.name;
                BasicChar loadedChar = loaded.GetComponent<BasicChar>();
                JsonUtility.FromJsonOverwrite(ds.who, loadedChar);
                ServantsDirty = true;
            }
            else
            {
                Debug.Log("Failed to load dorm servant...");
                // TODO add uneverial char to carry basicChar script
            }
        }
    }
}

[System.Serializable]
public class DormSave
{
    public string name;
    public string who;

    public DormSave(string Name, BasicChar Who)
    {
        name = Name;
        who = JsonUtility.ToJson(Who);
    }
}