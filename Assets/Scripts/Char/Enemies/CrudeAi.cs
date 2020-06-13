using UnityEngine;

public class CrudeAi : MonoBehaviour
{
    private enum State
    {
        Idle,
        Chase,
        Flee
    }

    [SerializeField] private PlayerHolder target = null;

    [SerializeField] private Rigidbody2D playerRigid = null, rb2d = null;

    private Vector2 Target => playerRigid.position;

    private Vector2 CurPos { get => rb2d.position; set => rb2d.position = value; }

    [SerializeField] private float chaseDist = 30f, movementSpeed = 5f;

    private State currentState;

    private void Start()
    {
        target = target != null ? target : PlayerHolder.GetPlayerHolder;
        playerRigid = playerRigid != null ? playerRigid : target.GetComponent<Rigidbody2D>();
        rb2d = rb2d != null ? rb2d : GetComponent<Rigidbody2D>();
        currentState = State.Idle;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                if (WithinChaseDist)
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
                    if (Physics.Linecast(CurPos, Target))
                    {
                        // doesn't work
                        // Debug.Log("Blocked");
                    }
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