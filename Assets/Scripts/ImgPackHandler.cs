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
    private static readonly List<GenderFolder> genderFolders = new List<GenderFolder>();
    private static readonly List<RaceFolder> raceFolders = new List<RaceFolder>();
    private static readonly List<string> acceptedEndSuffix = new List<string>() { ".jpg", ".png" };
    public static List<SceneInfo> FileInfos { get; private set; } = new List<SceneInfo>();

    // Start is called before the first frame update
    public static void SetupFolders()
    {
        path = Application.dataPath + "/ImgPack";
        dictInfo = !Directory.Exists(path) ? Directory.CreateDirectory(path) : new DirectoryInfo(path);
        // taken from stackoverflow
        Type[] listOfScenes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                               from assemblyType in domainAssembly.GetTypes()
                               where typeof(SexScenes).IsAssignableFrom(assemblyType)
                               select assemblyType).ToArray();

        foreach (Type abstractBaseClass in listOfScenes.Where(t => t.IsAbstract).Select(t => t))
        {
            string subPath = path + "/" + abstractBaseClass.FullName;
            subDictsInfos.Add(new DictPair(Directory.Exists(subPath) ? new DirectoryInfo(subPath) : dictInfo.CreateSubdirectory(subPath), abstractBaseClass));
        }

        GetSceneFolders(listOfScenes);
        GetGenderFolders();
        GetRaceFolders();
        GetImages();
    }

    private static void GetSceneFolders(Type[] listOfBs)
    {
        foreach (DictPair subInfo in subDictsInfos)
        {
            foreach (Type t in listOfBs.Where(t => !t.IsAbstract).Select(t => t))
            {
                DictPair subDict = subDictsInfos.Find(sd => sd.Type == t.BaseType);
                if (subDict != null)
                {
                    string scenePath = subDict.DirectoryInfo.FullName + "/" + t.FullName; // rf.Dir.FullName + "/" + t.FullName;
                    sceneFolders.Add(new SceneFolder(Directory.Exists(scenePath) ? new DirectoryInfo(scenePath) : subDict.DirectoryInfo.CreateSubdirectory(scenePath), t));
                }
            }
        }
    }

    private static void GetGenderFolders()
    {
        List<Genders> genders = UgreTools.EnumToList<Genders>();
        foreach (SceneFolder sI in sceneFolders)
        {
            foreach (Genders g in genders)
            {
                string genderPath = sI.Dir.FullName + "/" + g.ToString();
                genderFolders.Add(new GenderFolder(Directory.Exists(genderPath) ? new DirectoryInfo(genderPath) : sI.Dir.CreateSubdirectory(genderPath), sI.Type, g));
            }
        }
    }

    private static void GetRaceFolders()
    {
        List<Races> racesList = UgreTools.EnumToList<Races>();
        foreach (GenderFolder folder in genderFolders)
        {
            foreach (Races race in racesList)
            {
                string racePath = folder.Dir.FullName + "/" + race.ToString();
                raceFolders.Add(new RaceFolder(Directory.Exists(racePath) ? new DirectoryInfo(racePath) : folder.Dir.CreateSubdirectory(racePath), folder.Type, folder.Gender, race));
            }
        }
    }

    private static void GetImages()
    {
        sceneFolders.ForEach(sd =>
        {
            acceptedEndSuffix.ForEach(aes =>
            {
                sd.Dir.GetFiles("*" + aes).ToList().ForEach(fi => FileInfos.Add(new SceneInfo(fi, null, sd.Type)));
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

public readonly struct RaceFolder
{
    public RaceFolder(DirectoryInfo dir, Type type, Genders gender, Races race)
    {
        Dir = dir;
        Type = type;
        Gender = gender;
        Race = race;
    }

    public DirectoryInfo Dir { get; }
    public Type Type { get; }
    public Genders Gender { get; }
    public Races Race { get; }
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

public readonly struct GenderFolder
{
    public GenderFolder(DirectoryInfo dir, Type type, Genders gender)
    {
        Dir = dir;
        Type = type;
        Gender = gender;
    }

    public DirectoryInfo Dir { get; }
    public Type Type { get; }
    public Genders Gender { get; }
}

public class SceneFolder
{
    public DirectoryInfo Dir { get; private set; }
    public Type Type { get; private set; }

    public SceneFolder(DirectoryInfo dir, Type type)
    {
        this.Dir = dir;
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