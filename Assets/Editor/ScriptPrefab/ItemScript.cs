using System.IO;
using UnityEditor;
using UnityEngine;

public class CustomScript
{
    public static string PathToTemplate = Application.dataPath + @"/Editor/ScriptPrefab/ItemTemplate.cs";

    [MenuItem("Assets/ScriptTemplete/ItemTemplete")]
    public static void CreateCustomScript(MenuCommand cmd)
    {
        if (Selection.activeObject == null)
        {

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
            if (string.IsNullOrEmpty(path))
            {
                path = "Assets/";
            }
            File.Copy(PathToTemplate, Path.Combine(path, "NewScript.cs"));
            AssetDatabase.Refresh();
        }
    }
}

