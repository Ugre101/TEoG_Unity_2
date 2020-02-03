using System.IO;
using UnityEditor;
using UnityEngine;

public class CustomScript
{
    public static string PathToTemplate = Application.dataPath + @"/Editor/ScriptPrefab/ItemTemplate.cs";
    // private static string newScript = $"public class ItemTemplate : Misc {{\n public ItemTemplate() : base(ItemId.Potion, \"Template\")\n{{desc = \"template for items, desc itself is where you say what the item does. This item happens to do nothing.\";\n}} \n\n// use function aka what the item does, base.use() calls Use function of this class's base class (Item.Use) which does nothing.\npublic override string Use(BasicChar user)\n{{return base.Use(user);\n}}\n}}";

    [MenuItem("Assets/ScriptTemplete/ItemTemplete")]
    public static void CreateCustomScript(MenuCommand cmd)
    {
        if (Selection.activeObject != null)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
            if (string.IsNullOrEmpty(path))
            {
                path = "Assets/";
            }
            File.Copy(PathToTemplate, Path.Combine(path, "NewScript.cs")); ;
            AssetDatabase.Refresh();
        }
    }
}