using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowOneServant : MonoBehaviour
{
    private BasicChar basicChar;
    [SerializeField] private ShowDorm showDorm = null;
    [SerializeField] private Button backBtn = null, sexBtn = null, kickOutBtn = null;
    [SerializeField] private TextMeshProUGUI textBox = null;
    [SerializeField] private SexCanvas sexCanvas = null;
    [SerializeField] private PromptYesNo prompt = null;

    // Start is called before the first frame update
    private void Start()
    {
        showDorm = showDorm != null ? showDorm : GetComponentInParent<ShowDorm>();
        backBtn.onClick.AddListener(showDorm.ListServants);
        kickOutBtn.onClick.AddListener(KickOut);
        sexBtn.onClick.AddListener(DormSex);
    }

    public void Setup(BasicChar basicChar)
    {
        gameObject.SetActive(true);
        this.basicChar = basicChar;
        textBox.text = $"{basicChar.Identity.FullName}\n\n{basicChar.Summary()}\n\n{basicChar.BodyStats()}";
    }

    private void KickOut()
    {
        if (Dorm.Followers.TryGetValue(basicChar.Identity.Id, out BasicChar b))
        {
            Instantiate(prompt, transform).Setup(() => KickASevantOut(b));
        }
    }

    private void KickASevantOut(BasicChar basicChar)
    {
        Dorm.Followers.Remove(basicChar.Identity.Id);
        showDorm.ListServants();
    }

    private void DormSex() => sexCanvas.DormSex(basicChar);
}