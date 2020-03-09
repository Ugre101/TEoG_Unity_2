using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BuildButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private TextMeshProUGUI btnText = null;
    private BuildCost buildCost;
    private HomeUpgrade homeUpgrade;
    private BasicChar owner;
    private string buildingName;

    public void Setup(BuildCost buildCost, HomeUpgrade homeUpgrade, BasicChar owner, string buildingName)
    {
        this.buildCost = buildCost;
        this.homeUpgrade = homeUpgrade;
        this.owner = owner;
        this.buildingName = buildingName;
        DisplayCost();
        btn.onClick.AddListener(Upgrade);
    }

    private void DisplayCost() => btnText.text = $"{buildingName}: {buildCost.GetCost(homeUpgrade.Level).ToString()}g";

    private void Upgrade()
    {
        if (owner.Currency.TryToBuy(buildCost.GetCost(homeUpgrade.Level)))
        {
            homeUpgrade.Upgrade();
            DisplayCost();
        }
    }
}