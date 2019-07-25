using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapEvents : MonoBehaviour
{
    public delegate void WorldMapChange();
    public static event WorldMapChange worldMapChange;
    public playerMain player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Teleport(GameObject from, GameObject toWorld, Tilemap toMap)
    {
        from.SetActive(false);
        toWorld.SetActive(true);
        player.transform.position = toMap.localBounds.center;
    }
}
