using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class GetImages
{
    private static string path;
    private static DirectoryInfo dictInfo;
    private static List<DictPair> subDictsInfos = new List<DictPair>();

    // Start is called before the first frame update
    public static void SetupFolders()
    {
        path = Application.dataPath + "/ImgPack";
        if (!Directory.Exists(path))
        {
            dictInfo = Directory.CreateDirectory(path);
        }
        else
        {
            dictInfo = new DirectoryInfo(path);
        }
        var listOfBs = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                        from assemblyType in domainAssembly.GetTypes()
                        where typeof(SexScenes).IsAssignableFrom(assemblyType)
                        select assemblyType).ToArray();
        foreach (Type t in listOfBs)
        {
            if (t.IsAbstract)
            {
                string subPath = path + "/" + t.FullName;
                if (Directory.Exists(subPath))
                {
                    subDictsInfos.Add(new DictPair(new DirectoryInfo(subPath), t));
                }
                else
                {
                    subDictsInfos.Add(new DictPair(dictInfo.CreateSubdirectory(subPath), t));
                }
            }
        }
        subDictsInfos.ForEach(s => { Debug.Log(s.Type + " " + s.DirectoryInfo.FullName); });
        foreach (Type t in listOfBs)
        {
            if (!t.IsAbstract)
            {
                DirectoryInfo subDict = subDictsInfos.Find(sd => sd.Type == t.BaseType).DirectoryInfo;
                if (subDict != null)
                {
                    string subPath = subDict.FullName + "/" + t.FullName;
                    if (Directory.Exists(subPath))
                    {
                    }
                    else
                    {
                        _ = subDict.CreateSubdirectory(subPath);
                    }
                }
            }
        }
    }
}

public class DictPair
{
    public DirectoryInfo DirectoryInfo { get; private set; }
    public Type Type { get; private set; }

    public DictPair(DirectoryInfo directoryInfo, Type type)
    {
        this.DirectoryInfo = directoryInfo;
        this.Type = type;
    }
}