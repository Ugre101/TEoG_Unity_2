using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SexChar : MonoBehaviour
{
    [SerializeField]
    private BasicChar whom;

    [Header("Organ descs")]
    [SerializeField]
    private TextMeshProUGUI Dicks = null;

    [SerializeField]
    private TextMeshProUGUI Balls = null, Vagina = null, Boobs = null;

    [Header("Sliders")]
    [SerializeField]
    private MascSlider mascSlider = null;

    [SerializeField]
    private FemiSlider femiSlider = null;

    [SerializeField]
    private Slider ArousalSlider = null;

    [SerializeField]
    private TextMeshProUGUI OrgasmCounter = null;

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
        Organs sexualOrgans = whom.SexualOrgans;
        Dicks.text = sexualOrgans.Dicks.Looks();
        Balls.text = sexualOrgans.Balls.Looks();
        Boobs.text = sexualOrgans.Boobs.Looks();
        Vagina.text = sexualOrgans.Vaginas.Looks();
    }
}