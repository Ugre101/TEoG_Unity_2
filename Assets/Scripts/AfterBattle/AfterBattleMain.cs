using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AfterBattleMain : MonoBehaviour
{
    public playerMain player;
    public List<BasicChar> enemies;
    public TextMeshProUGUI _textBox;

    [SerializeField]
    private SexButton sexButton;

    #region Button containers

    [Header("Buttons containers")]
    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private GameObject DickActions;

    [SerializeField]
    private GameObject BoobsActions, VaginaActions, AssActions, HandActions,
        MouthActions, DrainActions, MiscActions;

    #endregion Button containers

    #region Scene lists

    [Header("ScriptableObject Scenes")]
    public List<SexScenes> dickScenes;

    public List<SexScenes> boobScenes, mouthScenes, vaginaScenes, analScenes;

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
        SceneChecker(buttons,
            new List<List<SexScenes>> { dickScenes, mouthScenes, boobScenes, vaginaScenes, analScenes });
        //   SceneChecker(MouthActions, mouthScenes);
        //   SceneChecker(BoobsActions, boobScenes);
        //   SceneChecker(VaginaActions, vaginaScenes);
        //   SceneChecker(AssActions, analScenes);
        Leave.SetActive(true);
        TakeHome.SetActive(dorm.CanTake(enemies[0]));
    }

    private void SceneChecker(GameObject container, List<List<SexScenes>> scenes)
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (List<SexScenes> list in scenes)
        {
            foreach (SexScenes scene in list)
            {
                if (scene.CanDo(player, Target))
                {
                    SexButton button = Instantiate(sexButton, container.transform);
                    button.Setup(player, Target, this, scene);
                }
            }
        }
    }

    public void AddToTextBox(string text)
    {
        _textBox.text = text;
    }
}