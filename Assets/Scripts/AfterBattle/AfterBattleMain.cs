using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AfterBattleMain : MonoBehaviour
{
    public playerMain player;
    public List<BasicChar> enemies;
    public TextMeshProUGUI _textBox;

    [SerializeField]
    private GameObject ButtonPrefab;

    [SerializeField]
    private GameObject ButtonPrefabMono;

    #region Button containers

    [Header("Buttons containers")]
    public GameObject DickActions;

    public GameObject BoobsActions;
    public GameObject VaginaActions;
    public GameObject AssActions;
    public GameObject HandActions;
    public GameObject MouthActions;
    public GameObject MiscActions;

    #endregion Button containers

    #region Scene lists

    [Header("ScriptableObject Scenes")]
    public List<SexScenes> dickScenes;

    public List<SexScenes> boobScenes;
    public List<SexScenes> mouthScenes;
    public List<SexScenes> vaginaScenes;
    public List<SexScenes> analScenes;

    #endregion Scene lists

    [Header("Other")]
    public SexScenes LastScene;

    public GameObject Leave;
    public GameObject TakeHome;
    public Dorm dorm;
    public SexChar playerChar, enemyChar;
    private BasicChar newTarget;
    public BasicChar Target => newTarget != null ? newTarget : enemies[0];

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        enemies.Clear();
    }

    public void Setup(List<BasicChar> chars)
    {
        gameObject.SetActive(true);
        enemies = chars;
        RefreshScenes();
        _textBox.text = null;
        LastScene = null;
        newTarget = null;
        // in future make it so several statuses spawn if team har more than one member.
        // if enemies more than one, make selector view next to status
        playerChar.Setup(player);
        enemyChar.Setup(Target);
    }

    private void RefreshScenes()
    {
        SceneChecker(DickActions, dickScenes);
        SceneChecker(MouthActions, mouthScenes);
        SceneChecker(BoobsActions, boobScenes);
        SceneChecker(VaginaActions, vaginaScenes);
        SceneChecker(AssActions, analScenes);
        Leave.SetActive(true);
        TakeHome.SetActive(dorm.CanTake(enemies[0]));
    }

    private void SceneChecker(GameObject container, List<SexScenes> scenes)
    {
        foreach (Transform child in container.transform)
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
                Instantiate(button, container.transform);
            }
        }
    }

    public void AddToTextBox(string text)
    {
        _textBox.text = text;
    }
}