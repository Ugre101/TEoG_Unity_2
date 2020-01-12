using UnityEngine;
using UnityEngine.UI;

public class ShowDorm : MonoBehaviour
{
    [SerializeField] private Dorm dorm = null;
    [SerializeField] private Transform container = null;
    [SerializeField] private ShowServant ServantListPrefab = null;
    [SerializeField] private GameObject upgrades = null, servantList = null;
    [SerializeField] private GameObject ifEmpty = null;
    [SerializeField] private Button upgradesBtn = null, back = null;

    // Start is called before the first frame update
    private void Start()
    {
        upgradesBtn.onClick.AddListener(UpgradePart);
        back.onClick.AddListener(ServantPart);
    }

    public void OnEnable()
    {
        ServantPart();
    }

    private void ServantPart()
    {
        upgrades.SetActive(false);
        back.gameObject.SetActive(false);
        upgradesBtn.gameObject.SetActive(true);
        servantList.SetActive(true);
        ListServants();
    }

    private void UpgradePart()
    {
        upgrades.SetActive(true);
        upgradesBtn.gameObject.SetActive(false);
        servantList.SetActive(false);
        back.gameObject.SetActive(true);
    }

    private void ListServants()
    {
        bool hasSevants = dorm.Servants.Count > 0;
        ifEmpty.SetActive(hasSevants);
        container.KillChildren();
        if (hasSevants)
        {
            dorm.Servants.ForEach(s =>
            {
                ShowServant showServant = Instantiate(ServantListPrefab, container);
                showServant.Init(s);
            });
        }
    }
}