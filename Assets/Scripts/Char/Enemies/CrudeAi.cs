using UnityEngine;

public class CrudeAi : MonoBehaviour
{
    private enum State
    {
        Idle,
        Chase,
        Flee
    }

    private PlayerSprite target = null;
    [SerializeField] private CharHolder thisHolder = null;
    [SerializeField] private Rigidbody2D playerRigid = null, rb2d = null;

    private Vector2 Target => playerRigid.position;
    private static int TargetLevel => PlayerMain.Player.Stats.CalcLevelByStatTotal();
    private int ThisBasicCharsLevel => thisHolder.BasicChar.Stats.CalcLevelByStatTotal();
    private Vector2 CurPos { get => rb2d.position; set => rb2d.position = value; }

    [SerializeField] private float chaseDist = 30f, movementSpeed = 5f;

    private State currentState;

    private void Start()
    {
        target = target != null ? target : PlayerSprite.Instance;
        thisHolder = thisHolder != null ? thisHolder : GetComponent<CharHolder>();
        playerRigid = playerRigid != null ? playerRigid : target.GetComponent<Rigidbody2D>();
        rb2d = rb2d != null ? rb2d : GetComponent<Rigidbody2D>();
        currentState = State.Idle;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                if (WithinChaseDist && ThisBasicCharsLevel + 10 > TargetLevel)
                {
                    currentState = State.Chase;
                    // If player much stronger maybe flee?
                }
                break;

            case State.Chase:
                if (!WithinChaseDist)
                {
                    CurPos = CurPos;
                    currentState = State.Idle;
                }
                else
                {
                    /*    if (Physics.Linecast(CurPos, Target))
                        {
                            // doesn't work
                            // Debug.Log("Blocked");
                        }*/
                    CurPos = Vector2.MoveTowards(CurPos, Target, movementSpeed * Time.deltaTime);
                }

                break;

            case State.Flee:
                currentState = State.Idle;
                break;

            default:
                currentState = State.Idle;
                break;
        }
    }

    private bool WithinChaseDist => Vector2.Distance(CurPos, Target) < chaseDist;
}