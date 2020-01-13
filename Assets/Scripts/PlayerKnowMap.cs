using UnityEngine;

public class PlayerKnowMap : MonoBehaviour
{
    [SerializeField] private GameObject banditMap = null;

    // Start is called before the first frame update
    private void Start()
    {
        PlayerFlags.BanditMap.KnowThisMap += BanditMap;
        BanditMap();
    }

    private void BanditMap() => banditMap.SetActive(PlayerFlags.BanditMap.Know);
}