using UnityEngine;
using UnityEngine.UI;

public class GameUIHelpBox : HelpBox
{
    private const string saveName = "GameUIHelp";

    protected override string SaveName => saveName;

}