using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LoseMain : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;

    [SerializeField] private TextMeshProUGUI _textBox = null;

    [SerializeField] private GameObject Leave = null;

    [SerializeField] private SexChar playerChar = null, enemyChar = null;

    [SerializeField] private Transform sexButtonsContainer = null;

    [SerializeField] private LoseSexButton sexButton = null;

    [SerializeField] private List<LoseScene> forced = new List<LoseScene>();

    [SerializeField] private List<LoseScene> submit = new List<LoseScene>();

    private List<BasicChar> enemies = new List<BasicChar>();

    private BasicChar newTarget = null;
    public BasicChar Target => newTarget != null ? newTarget : enemies[0];

    private List<LoseScene> CanDo(List<LoseScene> scenes) => scenes.Where(s => s.CanDo(player, Target)).Select(s => s).ToList();

    private readonly System.Random rnd = new System.Random();

    private LoseScene GetAScene(List<LoseScene> scenes) => scenes[rnd.Next(scenes.Count)];

    private void Start()
    {
        player = player ?? PlayerHolder.Player;
        LoseSexButton.PlayScene += HandleScene;
    }

    public void Setup(List<BasicChar> parEnemies)
    {
        gameObject.SetActive(true);
        enemies = parEnemies;
        _textBox.text = null;

        playerChar.Setup(player);
        enemyChar.Setup(Target);
        newTarget = null;
        Leave.SetActive(false);

        sexButtonsContainer.KillChildren();
        GetScenesForTarget();
    }

    public void AddToTextBox(string text) => _textBox.text = text;

    public void CanLeave()
    {
        Leave.SetActive(true);
        sexButtonsContainer.KillChildren();
    }

    private int SceneOptionsAmount = 1;

    private void GetScenesForTarget()
    {
        List<LoseScene> forcedCanDo = CanDo(forced);
        if (forcedCanDo.Count > 0)
        {
            for (int i = 0; i < SceneOptionsAmount; i++)
            {
                Instantiate(sexButton, sexButtonsContainer).Setup(player, Target, this, GetAScene(forcedCanDo));
            }
        }
        List<LoseScene> submitCanDo = CanDo(submit);
        if (submitCanDo.Count > 0)
        {
            for (int i = 0; i < SceneOptionsAmount; i++)
            {
                Instantiate(sexButton, sexButtonsContainer).Setup(player, Target, this, GetAScene(submitCanDo));
            }
        }
    }

    private void HandleScene(SexScenes scene)
    {
        AddToTextBox(scene.StartScene(player, Target));
        CanLeave();
    }
}