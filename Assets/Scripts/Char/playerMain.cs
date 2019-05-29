using UnityEngine;

public class playerMain : BasicChar
{
    // Start is called before the first frame update
    private void Start()
    {
        init(1, 100f, 100f);
        Str = 10;
        Charm = 10;
        Dex = 10;
        End = 10;
        Str++;
        AddDick();
        AddBalls();
        AddBoobs();
        AddVagina();
        Dicks[0].Grow();
        Vaginas[0].Grow();
        Femi.Gain(100f);
        Masc.Gain(100f);
        Dicks[0].Shrink(3);
    }
}