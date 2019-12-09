using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dorm : MonoBehaviour
{
    public Home home;

    public bool HasSpace => home.Stats.Dorm.Level * 3 > transform.childCount;

    public bool CanTake(ThePrey wannaTake) => HasSpace && wannaTake.SexStats.SessionOrgasm >= 0;

    private ThePrey[] ArrayServants => GetComponentsInChildren<ThePrey>();
    private bool ServantsDirty = true;

    [SerializeField]
    private List<ThePrey> lastServants;

    public List<ThePrey> Servants
    {
        get
        {
            if (ServantsDirty)
            {
                lastServants = new List<ThePrey>(ArrayServants);
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

    public List<GameObject> servantPrefabs;

    public List<DormSave> Save()
    {
        List<DormSave> dormSaves = new List<DormSave>();
        foreach (Transform child in transform)
        {
            DormSave tempDorm = new DormSave(child.name, child.GetComponent<ThePrey>());
            dormSaves.Add(tempDorm);
        }
        return dormSaves;
    }

    public void Load(List<DormSave> toLoad)
    {
        transform.KillChildren();
        foreach(DormSave ds in toLoad)
        {
            if (servantPrefabs.Exists(n => n.name == ds.name))
            {
                GameObject loaded = Instantiate(servantPrefabs.Find(n => n.name == ds.name), transform);
                loaded.name = ds.name;
                ThePrey loadedChar = loaded.GetComponent<ThePrey>();
                JsonUtility.FromJsonOverwrite(ds.who, loadedChar);
                ServantsDirty = true;
            }else
            {
                Debug.Log("Failed to load dorm servant...");
                // TODO add uneverial char to carry basicChar script
            }
        }
    }
}