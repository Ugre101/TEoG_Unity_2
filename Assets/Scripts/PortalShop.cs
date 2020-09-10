using System.Collections.Generic;
using UnityEngine;

public class PortalShop : Shop
{
    [SerializeField] private List<CanTelePortTo> telePorts = new List<CanTelePortTo>();
    [SerializeField] private BuyService sellServicePrefab = null;

    public override void Start()
    {
        base.Start();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        foreach (CanTelePortTo tele in telePorts)
        {
            if (!tele.Know)
            {
                string desc = $"Unlock teleport to\nWorld: {tele.World}\nMap: {tele.Map}";
                void Action()
                {
                    tele.Unlock();
                    OnEnable();
                }

                BuyServiceInfo buyService = new BuyServiceInfo("Teleport location", desc, 1000f, Action);
                wares.BuyServices(buyService);
            }
        }
    }

    public override void ShowWares()
    {
        base.ShowWares();
    }

    protected override void SellWares()
    {
        base.SellWares();
    }

    protected override void ToggleSelling()
    {
        base.ToggleSelling();
    }
}