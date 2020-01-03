using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImgPack : MonoBehaviour
{
    private RawImage raw;

    [SerializeField]
    private List<SceneInfo> fileInfos = new List<SceneInfo>();

    [SerializeField]
    private List<Texture2D> texture2Ds = new List<Texture2D>();

    // Start is called before the first frame update
    private void Start()
    {
        raw = raw != null ? raw : GetComponent<RawImage>();
        ImgPackHandler.SetupFolders();
        fileInfos.AddRange(ImgPackHandler.FileInfos);
        fileInfos.ForEach(fi =>
        {
            StartCoroutine(ImgPackHandler.GetTexture(fi.File.FullName, (Texture2D) => texture2Ds.Add(Texture2D)));
            Debug.Log(fi.File + " " + fi.Race + " " + fi.Type);
        });
    }
}