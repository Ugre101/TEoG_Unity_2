using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dorm : MonoBehaviour
{
    public int Level = 0;
    public int BaseCost = 0;
    public float CostMod = 1f;

    public int Cost
    {
        get
        {
            return Mathf.CeilToInt(BaseCost * CostMod);
        }
    }

    public bool Upgrade(int gold)
    {
        if (Cost > gold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasSpace
    {
        get
        {
            return Level * 3 > this.transform.childCount;
        }
    }

    public bool CanTake(BasicChar wannaTake)
    {
        return HasSpace && wannaTake.sexStats.SessionOrgasm >= 0;
    }

    private BasicChar[] arrayServants;
    private bool ServantsDirty = true;

    [SerializeField]
    private List<BasicChar> lastServants;

    public List<BasicChar> Servants
    {
        get
        {
            if (ServantsDirty)
            {
                arrayServants = GetComponentsInChildren<BasicChar>();
                lastServants = new List<BasicChar>(arrayServants);
                ServantsDirty = false;
            }
            return lastServants;
        }
    }

    public void AddTo(GameObject toAdd)
    {
        GameObject sercv = Instantiate(toAdd, this.transform);
        sercv.name = toAdd.name;
        ServantsDirty = true;
    }

    public List<GameObject> servantPrefabs;

    public void Load(DormSave toLoad)
    {
        if (servantPrefabs.Exists(n => n.name == toLoad.name))
        {
            GameObject loaded = Instantiate(servantPrefabs.Find(n => n.name == toLoad.name), this.transform);
            loaded.name = toLoad.name;
            BasicChar loadedChar = loaded.GetComponent<BasicChar>();
            JsonUtility.FromJsonOverwrite(toLoad.who, loadedChar);
            ServantsDirty = true;
        }
    }
}