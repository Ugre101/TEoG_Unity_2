using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Settings", menuName = "Tools")]
public class Settings : ScriptableObject
{
    [SerializeField]
    private bool Imperial = false;

    public void ToogleImp()
    {
        Imperial = Imperial ? false : true;
    }

    public string LorGal(float L)
    {
        if (Imperial)
        {
            return Mathf.Round(0.264172052f * L) < 1 ?
                $"{Mathf.Round(L * 4.22675284f)}cups" : $"{Mathf.Round(L * 0.264172052f)}gallon";
        }
        else
        {
            return L < 0.1f ? $"{Mathf.Round(L * 100)}cl" : L < 0.99 ? $"{Mathf.Round(L * 10)}dl" : $"{Mathf.Round(L)}L";
        }
    }

    public string MorInch(float cm)
    {
        if (Imperial)
        {
            float Inch = Mathf.Round(cm / 2.54f);
            float Feet = Mathf.Floor(Inch / 12);
            float Yard = Mathf.Floor(Feet / 3);
            return Yard > 0 ? $"{Yard}yard" : Feet > 0 ? $"{Feet}feet" : Inch > 0 ? $"{Inch}inches" : $"";
        }
        else
        {
            float m = Mathf.Floor(cm / 100);
            return m > 5f ? $"{m}m" : $"{cm}cm";
        }
    }

    public string DickLook(Dick d)
    {
        return $"a {MorInch(d.Size)} long dick";
    }
}