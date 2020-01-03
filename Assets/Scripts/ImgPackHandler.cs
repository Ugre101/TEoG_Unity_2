using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class ImgPackHandler
{
    private static string path;
    private static DirectoryInfo dictInfo;
    private static readonly List<DictPair> subDictsInfos = new List<DictPair>();
    private static readonly List<SceneFolder> sceneFolders = new List<SceneFolder>();
    private static readonly List<string> acceptedEndSuffix = new List<string>() { ".jpg", ".png" };
    public static List<SceneInfo> FileInfos { get; private set; } = new List<SceneInfo>();

    // Start is called before the first frame update
    public static void SetupFolders()
    {
        path = Application.dataPath + "/ImgPack";
        dictInfo = !Directory.Exists(path) ? Directory.CreateDirectory(path) : new DirectoryInfo(path);
        // taken from stackoverflow
        Type[] listOfBs = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                           from assemblyType in domainAssembly.GetTypes()
                           where typeof(SexScenes).IsAssignableFrom(assemblyType)
                           select assemblyType).ToArray();

        foreach (Type t in listOfBs.Where(t => t.IsAbstract).Select(t => t))
        {
            string subPath = path + "/" + t.FullName;
            subDictsInfos.Add(new DictPair(Directory.Exists(subPath) ? new DirectoryInfo(subPath) : dictInfo.CreateSubdirectory(subPath), t));
        }
        GetRaceFolders();
        GetSceneFolders(listOfBs);
        GetImages();
    }

    private static void GetRaceFolders()
    {
        List<Races> racesList = Enum.GetValues(typeof(Races)).OfType<Races>().ToList();
        subDictsInfos.ForEach(sd =>
        {
            racesList.ForEach(r =>
            {
                string racePath = sd.DirectoryInfo.FullName + "/" + r.ToString();
                sd.RaceFolders.Add(new RaceFolder(Directory.Exists(racePath) ? new DirectoryInfo(racePath) : sd.DirectoryInfo.CreateSubdirectory(racePath), r));
            });
            string defaultPath = sd.DirectoryInfo.FullName + "/Default";
            sd.RaceFolders.Add(new RaceFolder(Directory.Exists(defaultPath) ? new DirectoryInfo(defaultPath) : sd.DirectoryInfo.CreateSubdirectory(defaultPath), null));
        });
    }

    private static void GetSceneFolders(Type[] listOfBs)
    {
        foreach (Type t in listOfBs.Where(t => !t.IsAbstract).Select(t => t))
        {
            DictPair subDict = subDictsInfos.Find(sd => sd.Type == t.BaseType);
            if (subDict != null)
            {
                subDict.RaceFolders.ForEach(rf =>
                {
                    string subPath = rf.Dir.FullName + "/" + t.FullName;
                    sceneFolders.Add(new SceneFolder(Directory.Exists(subPath) ? new DirectoryInfo(subPath) : rf.Dir.CreateSubdirectory(subPath), rf.Race ?? null, t));
                });
            }
        }
    }

    private static void GetImages()
    {
        sceneFolders.ForEach(sd =>
        {
            acceptedEndSuffix.ForEach(aes =>
            {
                sd.Dir.GetFiles("*" + aes).ToList().ForEach(fi => FileInfos.Add(new SceneInfo(fi, sd.Race, sd.Type)));
            });
        });
    }

    public static IEnumerator GetTexture(string path, Action<Texture2D> callback)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                callback?.Invoke(texture);
            }
        }
    }
}

public class DictPair
{
    public DirectoryInfo DirectoryInfo { get; private set; }
    public Type Type { get; private set; }
    public List<RaceFolder> RaceFolders { get; private set; } = new List<RaceFolder>();

    public DictPair(DirectoryInfo directoryInfo, Type type)
    {
        this.DirectoryInfo = directoryInfo;
        this.Type = type;
    }
}

public class RaceFolder
{
    public DirectoryInfo Dir { get; private set; }
    public Races? Race { get; private set; }

    public RaceFolder(DirectoryInfo dir, Races? race)
    {
        this.Dir = dir;
        this.Race = race;
    }
}

public class ImagePair
{
    public Type Type { get; private set; }
    public Sprite Sprite { get; private set; }

    public ImagePair(Type type, Sprite sprite)
    {
        this.Type = type;
        this.Sprite = sprite;
    }
}

public class SceneFolder
{
    public DirectoryInfo Dir { get; private set; }
    public Races? Race { get; private set; }
    public Type Type { get; private set; }

    public SceneFolder(DirectoryInfo dir, Races? race, Type type)
    {
        this.Dir = dir;
        this.Race = race;
        this.Type = type;
    }
}

public class SceneInfo
{
    public FileInfo File { get; private set; }
    public Races? Race { get; private set; }
    public Type Type { get; private set; }

    public SceneInfo(FileInfo fileInfo, Races? race, Type type)
    {
        this.File = fileInfo;
        this.Race = race;
        this.Type = type;
    }
}