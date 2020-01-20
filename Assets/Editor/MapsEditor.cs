using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Map))]
public class MapsEditor : Editor
{
    private Rect dropArea;
    private Map map;
    private SerializedProperty enemies;

    private void OnEnable()
    {
        map = (Map)target;
        enemies = serializedObject.FindProperty("enemies");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(10);
        dropArea = GUILayoutUtility.GetRect(0.0f, 50f, GUILayout.ExpandWidth(true));
        DropAreaGUI();
    }

    public void DropAreaGUI()
    {
        Event evt = Event.current;
        GUI.Box(dropArea, "Drop enemyprefab");
        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition))
                {
                    return;
                }
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    foreach (Object dragged in DragAndDrop.objectReferences)
                    {
                        if (dragged is GameObject go)
                        {
                            if (go.GetComponent<EnemyPrefab>() is EnemyPrefab test)
                            {
                                map.Enemies.Add(test);
                            }
                        }
                    }
                }
                break;
        }
    }
}