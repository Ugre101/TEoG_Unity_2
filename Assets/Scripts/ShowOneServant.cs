using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowOneServant : MonoBehaviour
{
    private BasicChar basicChar;
    [SerializeField] private ShowDorm showDorm = null;
    [SerializeField] private Dorm dorm = null;
    [SerializeField] private Button backBtn = null, kickOutBtn = null;
    [SerializeField] private TextMeshProUGUI textBox = null;

    [SerializeField] private PromptYesNo prompt = null;

    // Start is called before the first frame update
    private void Start()
    {
        dorm = dorm != null ? dorm : Dorm.GetDrom;
        showDorm = showDorm != null ? showDorm : GetComponentInParent<ShowDorm>();
        backBtn.onClick.AddListener(showDorm.ListServants);
        kickOutBtn.onClick.AddListener(KickOut);
    }

    public void Setup(BasicChar basicChar)
    {
        gameObject.SetActive(true);
        this.basicChar = basicChar;
        textBox.text = $"{basicChar.Identity.FullName}\n\n{basicChar.Summary()}\n\n{basicChar.BodyStats()}";
    }

    private void KickOut()
    {
        if (dorm.Servants.Exists(b => b == basicChar))
        {
            BasicChar who = dorm.Servants.Find(b => b == basicChar);
            Instantiate(prompt, transform).Setup(
                () =>
                {
                    dorm.Servants.Remove(who);
                    Destroy(who.gameObject);
                    showDorm.ListServants();
                });
        }
    }
}