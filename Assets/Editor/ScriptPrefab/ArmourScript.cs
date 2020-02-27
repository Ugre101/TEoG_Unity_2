using UnityEditor;
using UnityEngine;

public class ArmourScript
{
    private static readonly string PathToTemplate = Application.dataPath + @"/Editor/ScriptPrefab/ArmourTemplate.cs";

    [MenuItem("Assets/ScriptTemplete/ArmourTemplate")]
    public static void CreateCustomScript(MenuCommand cmd)
    {
        UgreEditorTools.SpawnCopyOfScript(PathToTemplate);
    }
}