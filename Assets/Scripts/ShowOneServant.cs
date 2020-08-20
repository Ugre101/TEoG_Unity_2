using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowOneServant : MonoBehaviour
{
    private DormMate dormMate;
    private BasicChar mate => dormMate.BasicChar;
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

    public void Setup(DormMate dormMate)
    {
        gameObject.SetActive(true);
        this.dormMate = dormMate;
        textBox.text = $"{mate.Identity.FullName}\n\n{mate.Summary()}\n\n{mate.BodyStats()}";
    }

    private void KickOut()
    {
        if (Dorm.Followers.TryGetValue(mate.Identity.Id, out DormMate b))
        {
            Instantiate(prompt, transform).Setup(() => KickASevantOut(b));
        }
    }

    private void KickASevantOut(DormMate dormMate)
    {
        Dorm.Followers.Remove(dormMate.BasicChar.Identity.Id);
        showDorm.ListServants();
    }

    private void DormSex() => sexCanvas.DormSex(mate);
}