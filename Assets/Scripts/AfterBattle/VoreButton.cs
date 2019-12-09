using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vore;
public class VoreButton : MonoBehaviour
{
    private AfterBattleMain afterBattleMain;
    public VoreScene voreScene;
    public TextMeshProUGUI textMeshPro;

    [SerializeField]
    private Button btn;

    private PlayerMain player;
    private BasicChar other;
    // Start is called before the first frame update
    private void Start()
    {
        if (btn == null)
        {
            btn = GetComponent<Button>();
        }
    }

    private void Vore()
    {
        afterBattleMain.AddToTextBox(voreScene.Vore(player, other));
        afterBattleMain.LastScene = voreScene;
        VoredEvent?.Invoke();
    }

    public void Setup(PlayerMain parPlayer, BasicChar parPartner, AfterBattleMain parAfterBattle, VoreScene parScene)
    {
        player = parPlayer;
        other = parPartner;
        afterBattleMain = parAfterBattle;
        voreScene = parScene;
        textMeshPro.text = voreScene.name;
        btn.onClick.AddListener(Vore);
    }

    public delegate void Vored();

    public static event Vored VoredEvent;
}