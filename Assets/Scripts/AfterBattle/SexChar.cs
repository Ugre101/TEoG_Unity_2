using SexCharStuff;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SexChar : MonoBehaviour
{
    private BasicChar whom;

    [Header("Organ descs")]
    [SerializeField] private DickInfo dickInfo = null;

    [SerializeField] private BallsInfo ballsInfo = null;
    [SerializeField] private BoobsInfo boobsInfo = null;
    [SerializeField] private VaginaInfo vaginaInfo = null;

    [Header("Sliders")]
    [SerializeField] private MascSlider mascSlider = null;

    [SerializeField] private FemiSlider femiSlider = null;
    [SerializeField] private Slider ArousalSlider = null;
    [SerializeField] private TextMeshProUGUI OrgasmCounter = null;

    public void OnDisable()
    {
        whom.SexStats.ArousalChangeEvent -= Arousal;
        whom.SexualOrgans.Dicks.Change -= DickInfo;
        whom.SexualOrgans.Balls.Change -= BallsInfo;
        whom.SexualOrgans.Vaginas.Change -= VaginaInfo;
        whom.SexualOrgans.Boobs.Change -= BoobsInfo;
    }

    public void Setup(BasicChar basicChar)
    {
        whom = basicChar;
        mascSlider.Init(whom);
        femiSlider.Init(whom);
        AllOrgans();
        Arousal();
        whom.SexStats.ArousalChangeEvent += Arousal;
        whom.SexualOrgans.Dicks.Change += DickInfo;
        whom.SexualOrgans.Balls.Change += BallsInfo;
        whom.SexualOrgans.Vaginas.Change += VaginaInfo;
        whom.SexualOrgans.Boobs.Change += BoobsInfo;
    }

    private void Arousal()
    {
        ArousalSlider.value = whom.SexStats.ArousalSlider;
        OrgasmCounter.text = whom.SexStats.SessionOrgasm.ToString();
    }

    private void AllOrgans()
    {
        DickInfo();
        BallsInfo();
        BoobsInfo();
        VaginaInfo();
    }

    private void VaginaInfo() => vaginaInfo.Setup(whom);

    private void BoobsInfo() => boobsInfo.Setup(whom);

    private void BallsInfo() => ballsInfo.Setup(whom);

    private void DickInfo() => dickInfo.Setup(whom);
}