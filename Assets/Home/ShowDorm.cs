using UnityEngine;
using UnityEngine.UI;

public class ShowDorm : MonoBehaviour
{
    public Dorm dorm;
    public GameObject container;
    public GameObject ServantListPrefab;
    public GameObject upgrades, servantList;
    public GameObject ifEmpty;
    public Button upgradesBtn, back;

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
        if (dorm.Servants.Count > 0)
        {
            ifEmpty.SetActive(false);
            foreach (Transform child in container.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (ThePrey Serv in dorm.Servants)
            {
                GameObject test = Instantiate(ServantListPrefab, container.transform);
                ShowServant showServant = test.GetComponent<ShowServant>();
                showServant.Init(Serv);
            }
        }
        else
        {
            ifEmpty.SetActive(true);
        }
    }
}