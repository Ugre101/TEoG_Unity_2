using System;
using UnityEngine;

public class EnterBuilding : MonoBehaviour
{
    [SerializeField] private Building building = null;

    public GameObject BuildingToEnter => building.gameObject;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(PlayerSprite.Instance.gameObject.tag))
        {
            if (building != null)
            {
                Enter?.Invoke(building);
                // gameUI.EnterBuilding(BuildingToEnter);
            }
        }
    }

    public static Action<Building> Enter;
}