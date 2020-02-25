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

    private class LoadedChar
    {
        public LoadedChar(BasicChar basicChar, DormSave dormSave)
        {
            Loaded = basicChar;
            Save = dormSave;
        }

        public BasicChar Loaded { get; }
        public DormSave Save { get; }
    }

    private List<LoadedChar> loadedChars = new List<LoadedChar>();

    public void Load(List<DormSave> toLoad)
    {
        transform.KillChildren();
        if (toLoad.Count > 0)
        {
            foreach (DormSave ds in toLoad)
            {
                if (servantPrefabs.Exists(n => n.name == ds.Name))
                {
                    BasicChar loaded = AddTo(servantPrefabs.Find(n => n.name == ds.Name));
                    loadedChars.Add(new LoadedChar(loaded, ds));
                }
                else
                {
                    BasicChar loaded = AddTo(defaultPrefab);
                    loadedChars.Add(new LoadedChar(loaded, ds));
                }
            }
            StartCoroutine(WaitAFrame());
        }
    }

    private IEnumerator WaitAFrame()
    {
        // wait a frame to let new basicchar to fully load before overwritting it
        yield return new WaitForEndOfFrame();
        foreach (LoadedChar loaded in loadedChars)
        {
            JsonUtility.FromJsonOverwrite(loaded.Save.Who, loaded.Loaded);
        }
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