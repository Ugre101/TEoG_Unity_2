using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class CustomScript
{
    public static string PathToTemplate = Application.dataPath + @"/Editors\ScriptPrefab\ItemTemplate.cs";

    [MenuItem("Assets/Create My Custom C# Script")]
    public static void CreateCustomScript(MenuCommand cmd)
    {
        if (Selection.activeObject == null) ;
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (File.Exists(path))
        {
            path = Path.GetDirectoryName(path);
        }
        if(string.IsNullOrEmpty(path))
        {
            path = "Assets/";
        }
        File.Copy(PathToTemplate, Path.Combine(path, "NewScript.cs"));
        AssetDatabase.Refresh();
    }

    public void Start()
    {

    }

}
