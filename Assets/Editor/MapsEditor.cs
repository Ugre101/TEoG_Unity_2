using System.IO;
using UnityEditor;
using UnityEngine;

namespace EditorStuff
{
    public static class MapsEditorFoldouts
    {
        public static bool EnemiesFoldout { get; set; } = false;
        public static bool BossesFoldout { get; set; } = false;
    }

    [CustomEditor(typeof(Map))]
    public class MapsEditor : Editor
    {
        private Rect dropEnemies, dropBosses, dropFolder;
        private Map map;
        private SerializedProperty mapName, amountOfEnemies, enemies, bosses;
        private bool EFold { get => MapsEditorFoldouts.EnemiesFoldout; set => MapsEditorFoldouts.EnemiesFoldout = value; }
        private bool BFold { get => MapsEditorFoldouts.BossesFoldout; set => MapsEditorFoldouts.BossesFoldout = value; }

        private void OnEnable()
        {
            map = (Map)target;
            mapName = serializedObject.FindProperty("mapName");
            amountOfEnemies = serializedObject.FindProperty("amountOfEnemies");
            enemies = serializedObject.FindProperty("enemies");
            bosses = serializedObject.FindProperty("bosses");
        }

        public override void OnInspectorGUI()
        {
            //  base.OnInspectorGUI();
            serializedObject.UpdateIfRequiredOrScript();
            EditorGUILayout.PropertyField(mapName);
            EditorGUILayout.PropertyField(amountOfEnemies);
            // EditorGUILayout.PropertyField(enemies);
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Enemies", EditorStyles.boldLabel);
            for (int i = 0; i < enemies.arraySize; i++)
            {
                EditorGUILayout.PropertyField(enemies.GetArrayElementAtIndex(i));
            }
            dropEnemies = GUILayoutUtility.GetRect(0.0f, 40f, GUILayout.ExpandWidth(true));
            //  DropAreaGUI();
            UgreEditorTools.DropAreaGUI(dropEnemies, "Drop enemyprefab and normal enemies", HandleDroppedEnemies);
            GUILayout.Space(10);
            dropFolder = GUILayoutUtility.GetRect(0.0f, 40f, GUILayout.ExpandWidth(true));
            UgreEditorTools.DropAreaGUI(dropFolder, "Test drop folder of enemies", FindItems);
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Bosses", EditorStyles.boldLabel);
            for (int i = 0; i < bosses.arraySize; i++)
            {
                EditorGUILayout.PropertyField(bosses.GetArrayElementAtIndex(i));
            }
            dropBosses = GUILayoutUtility.GetRect(0.0f, 40f, GUILayout.ExpandWidth(true));
            UgreEditorTools.DropAreaGUI(dropBosses, "Drop bosses", HandleDroppedBosses);
            serializedObject.ApplyModifiedProperties();
        }

        private void HandleDroppedEnemies(Object obj)
        {
            if (obj is GameObject go)
            {
                // skipt gettype so stuff derived from prefab can be added.
                if (go.GetComponent<EnemyPrefab>() is EnemyPrefab test) //  && test.GetType() == typeof(EnemyPrefab)
                {
                    map.Enemies.Add(test);
                }
            }
        }

        private void HandleDroppedBosses(Object obj)
        {
            if (obj is GameObject go)
            {
                if (go.GetComponent<Boss>() is Boss boss)
                {
                    map.Bosses.Add(boss);
                }
            }
        }

        private void FindItems(Object obj)
        {
            map.EnemyFolder = obj;
            string assetPath = AssetDatabase.GetAssetPath(obj);
            string fileName = Path.GetFileName(assetPath + obj.name);
            string dictName = assetPath.Replace(fileName, "");
            string folderName = dictName;
            DirectoryInfo toInclude = new DirectoryInfo(folderName);
            foreach (FileInfo fileInfo in toInclude.GetFiles())
            {
                var temp = AssetDatabase.LoadAssetAtPath(folderName + "/" + fileInfo.Name, typeof(EnemyPrefab));
                if (temp is EnemyPrefab enemy)
                {
                    map.Enemies.Add(enemy);
                }
            }
        }
    }
}