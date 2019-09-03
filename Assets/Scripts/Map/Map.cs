using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Maps
{
    Start,
    ToVillage,
    Village,
    ToWitch,
    WitchWood,
    WitchHut,
    Forest,
    DeepForest
}
public class Map : MonoBehaviour
{
    public Maps map;
    public string testMap{get { return this.transform.name;  }}
}
