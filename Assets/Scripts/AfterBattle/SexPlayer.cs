using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SexChar : MonoBehaviour
{
    public BasicChar whom;
    [Header("Organ descs")]
    public TextMeshProUGUI Dicks;
    public TextMeshProUGUI Balls;
    public TextMeshProUGUI Vagina;
    public TextMeshProUGUI Boobs;
    [Header("Sliders")]
    public MascSlider mascSlider;
    public FemiSlider femiSlider;
    public Slider ArousalSlider;
    public TextMeshProUGUI OrgasmCounter;
    public virtual void OnEnable()
    {
        mascSlider.Init(whom);
        femiSlider.Init(whom);
        SexStats.arousalChange += Arousal;
        Organs();
    }
    private void Arousal()
    {
        ArousalSlider.value = whom.sexStats.ArousalSlider();
        OrgasmCounter.text = whom.sexStats.SessionOrgasm.ToString();
    }
    private void Organs()
    {
        Dicks.text = whom.Looks.DicksLook();
    }
}
public class SexPlayer : SexChar
{

}
