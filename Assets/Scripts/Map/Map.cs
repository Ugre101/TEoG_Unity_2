using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private string mapName = string.Empty;

    [Tooltip("Amount of ememies to spawn.")]
    [SerializeField] private int amountOfEnemies = 6;

    [Header("Map enemies")]
    [Tooltip("Types of enemies that can spawn on this map.")]
    [SerializeField] private List<EnemyPrefab> enemies = new List<EnemyPrefab>();

    [Header("Bosses")]
    [SerializeField] private List<Boss> bosses = new List<Boss>();

    public string MapTitle => mapName;

    public List<EnemyPrefab> Enemies => enemies;
    public List<Boss> Bosses => bosses;
    public int EnemyCount => amountOfEnemies;

    public Object EnemyFolder { get => enemyFolder; set => enemyFolder = value; }

    [SerializeField] private Object enemyFolder = null;

    private void Start()
    {
        foreach (EnemyPrefab enemyPrefab in Enemies)
        {
            if (enemyPrefab == null)
            {
                Debug.LogWarning(name + " is missing enemys refs");
                if (EnemyFolder != null && Debug.isDebugBuild)
                {
                    string assetPath = AssetDatabase.GetAssetPath(EnemyFolder);
                    string fileName = Path.GetFileName(assetPath + EnemyFolder.name);
                    string dictName = assetPath.Replace(fileName, "");
                    string folderName = dictName;
                    DirectoryInfo toInclude = new DirectoryInfo(folderName);
                    foreach (FileInfo fileInfo in toInclude.GetFiles())
                    {
                        var temp = AssetDatabase.LoadAssetAtPath(folderName + "/" + fileInfo.Name, typeof(EnemyPrefab));
                        if (temp is EnemyPrefab enemy)
                        {
                            Enemies.Add(enemy);
                        }
                    }
                }
                break;
            }
        }
        foreach (Boss boss in Bosses)
        {
            if (boss == null)
            {
                Debug.LogWarning(name + " is missing boss refs");
                if (EnemyFolder != null && Debug.isDebugBuild)
                {
                    string assetPath = AssetDatabase.GetAssetPath(EnemyFolder);
                    string fileName = Path.GetFileName(assetPath + EnemyFolder.name);
                    string dictName = assetPath.Replace(fileName, "");
                    string folderName = dictName;
                    DirectoryInfo toInclude = new DirectoryInfo(folderName);
                    foreach (FileInfo fileInfo in toInclude.GetFiles())
                    {
                        var temp = AssetDatabase.LoadAssetAtPath(folderName + "/" + fileInfo.Name, typeof(Boss));
                        if (temp is Boss enemy)
                        {
                            Bosses.Add(enemy);
                        }
                    }
                }
                break;
            }
        }
    }
}