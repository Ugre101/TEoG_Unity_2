using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SexChar : MonoBehaviour
{
    public BasicChar whom;

    [Header("Organ descs")]
    [SerializeField]
    private TextMeshProUGUI Dicks;
    [SerializeField]
    private TextMeshProUGUI Balls, Vagina, Boobs;

    [Header("Sliders")]
    public MascSlider mascSlider;

    public FemiSlider femiSlider;
    public Slider ArousalSlider;
    public TextMeshProUGUI OrgasmCounter;

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
        SexStats.arousalChange -= Arousal;
        SexualOrgan.SomethingChanged -= Organs;
    }

    public void Setup(BasicChar basicChar)
    {
        whom = basicChar;
        mascSlider.Init(whom);
        femiSlider.Init(whom);
        Organs();
        SexStats.arousalChange += Arousal;
        SexualOrgan.SomethingChanged += Organs;
    }

    private void Arousal()
    {
        ArousalSlider.value = whom.sexStats.ArousalSlider();
        OrgasmCounter.text = whom.sexStats.SessionOrgasm.ToString();
    }

    private void Organs()
    {
        Dicks.text = whom.Looks.DicksLook();
        Balls.text = whom.Looks.BallsLook();
        Boobs.text = whom.Looks.BoobsLook();
        Vagina.text = whom.Looks.VagsLook();
    }
}