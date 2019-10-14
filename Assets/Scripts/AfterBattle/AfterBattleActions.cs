using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AfterBattleActions : MonoBehaviour
{
    public playerMain player;
    public List<BasicChar> enemies = new List<BasicChar>();
    public TextMeshProUGUI _textBox;
    public GameObject ButtonPrefab;
    public GameObject ButtonPrefabMono;

    [Header("Buttons containers")]
    public GameObject DickActions;

    public GameObject BoobsActions;
    public GameObject VaginaActions;
    public GameObject AssActions;
    public GameObject HandActions;
    public GameObject MouthActions;
    public GameObject MiscActions;

    [Header("ScriptableObject Scenes")]
    public List<SexScenes> dickScenes;

    public List<SexScenes> boobScenes;
    public List<SexScenes> mouthScenes;
    public List<SexScenes> vaginaScenes;
    public List<SexScenes> analScenes;

    [Header("Other")]
    public SexScenes LastScene;

    public GameObject Leave;
    public GameObject TakeHome;
    public Dorm dorm;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        if (_textBox == null)
        {
            this.enabled = false;
        }
        _textBox.text = null;
        if (enemies.Count > 0)
        {
            RefreshScenes();
        }
        LastScene = null;
    }

    private void OnDisable()
    {
        enemies.Clear();
    }

    private void RefreshScenes()
    {
        SceneChecker(DickActions.transform, dickScenes);
        SceneChecker(MouthActions.transform, mouthScenes);
        SceneChecker(BoobsActions.transform, boobScenes);
        SceneChecker(VaginaActions.transform, vaginaScenes);
        SceneChecker(AssActions.transform, analScenes);
        if (true)
        {
            Leave.SetActive(true);
        }
        TakeHome.SetActive(dorm.CanTake(enemies[0]));
    }

    private void SceneChecker(Transform container, List<SexScenes> scenes)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        foreach (SexScenes scene in scenes)
        {
            if (scene.CanDo(player, enemies[0]))
            {
                GameObject button = ButtonPrefab;
                TextMeshProUGUI title = button.GetComponentInChildren<TextMeshProUGUI>();
                title.text = scene.name;
                SexButton sexBtn = button.GetComponent<SexButton>();
                sexBtn.scene = scene;
                Instantiate(button, container);
            }
        }
    }

    public void AddToTextBox(string text)
    {
        _textBox.text = text;
    }
}