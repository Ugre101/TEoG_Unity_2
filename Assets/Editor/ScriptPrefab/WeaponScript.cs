using UnityEditor;
using UnityEngine;

public class WeaponScript
{
    private static readonly string PathToTemplate = Application.dataPath + @"/Editor/ScriptPrefab/WeaponTemplate.cs";

    [MenuItem("Assets/ScriptTemplete/WeaponTemplate")]
    public static void CreateCustomScript(MenuCommand cmd)
    {
        UgreEditorTools.SpawnCopyOfScript(PathToTemplate);
    }
}