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
        SexualOrgan.SomethingChanged -= Organs;
    }

    public void Setup(BasicChar basicChar)
    {
        whom = basicChar;
        mascSlider.Init(whom);
        femiSlider.Init(whom);
        Organs();
        Arousal();
        whom.SexStats.ArousalChangeEvent += Arousal;
        SexualOrgan.SomethingChanged += Organs;
    }

    private void Arousal()
    {
        ArousalSlider.value = whom.SexStats.ArousalSlider;
        OrgasmCounter.text = whom.SexStats.SessionOrgasm.ToString();
    }

    private void Organs()
    {
        dickInfo.Setup(whom);
        ballsInfo.Setup(whom);
        boobsInfo.Setup(whom);
        vaginaInfo.Setup(whom);
    }
}