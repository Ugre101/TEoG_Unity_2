using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImgPack : MonoBehaviour
{
    private RawImage raw;

    [SerializeField] private List<SceneInfo> fileInfos = new List<SceneInfo>();

    [SerializeField] private List<Texture2D> texture2Ds = new List<Texture2D>();

    // Start is called before the first frame update
    private void Start()
    {
        raw = raw != null ? raw : GetComponent<RawImage>();
        ImgPackHandler.SetupFolders();
        fileInfos.AddRange(ImgPackHandler.FileInfos);
        foreach (SceneInfo fi in fileInfos)
        {
            StartCoroutine(ImgPackHandler.GetTexture(fi.File.FullName, (texture2D) => texture2Ds.Add(texture2D)));
        }
    }

    [SerializeField]
    private bool testing = false;

    [SerializeField]
    private int testIndex = 0;

    public void Update()
    {
        if (testing && texture2Ds.Count > 0)
        {
            Texture2D test = texture2Ds[Mathf.Clamp(testIndex, 0, texture2Ds.Count - 1)];
            float ratio = (float)test.width / test.height;
            raw.rectTransform.sizeDelta = new Vector2(100 * ratio, 100);
        }
    }
}