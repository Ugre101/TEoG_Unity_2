using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharSprites))]
public class CharSpritesEditor : Editor
{
    private CharSprites myTarget;
    private SerializedProperty defaultSprite;
    private SerializedProperty list;

    private void OnEnable()
    {
        myTarget = (CharSprites)target;
        defaultSprite = serializedObject.FindProperty("defaultSprite");
        list = serializedObject.FindProperty("charSprites");
        //  FindItems();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("Default sprite: ", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(defaultSprite);
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Char sprites: ", EditorStyles.boldLabel);
        if (GUILayout.Button("Add all sprites in this folder", GUILayout.Height(30)))
        {
            FindItems();
        }
        EditorGUILayout.Space(2);
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void FindItems()
    {
        string assetPath = AssetDatabase.GetAssetPath(myTarget);
        string fileName = Path.GetFileName(assetPath);
        string folderName = assetPath.Replace(fileName, "");
        DirectoryInfo toInclude = new DirectoryInfo(folderName);
        foreach (FileInfo fileInfo in toInclude.GetFiles())
        {
            var temp = AssetDatabase.LoadAssetAtPath(folderName + "/" + fileInfo.Name, typeof(CharSprite));
            if (temp is CharSprite charSprite)
            {
                if (!myTarget.List.Exists(cs => cs == charSprite))
                {
                    myTarget.List.Add(charSprite);
                }
            }
        }
    }
}