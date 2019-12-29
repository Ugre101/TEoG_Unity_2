using UnityEngine;

public class CrudeAi : MonoBehaviour
{
    private enum State
    {
        Idle,
        Chase,
        Flee
    }

    [SerializeField]
    private PlayerMain target;

    [SerializeField]
    private Rigidbody2D playerRigid;

    private Vector2 Target => playerRigid.position;

    [SerializeField]
    private Rigidbody2D rb2d = null;

    private Vector2 CurPos { get => rb2d.position; set => rb2d.position = value; }

    [SerializeField]
    private float chaseDist = 30f;

    [SerializeField]
    private float movementSpeed = 5f;

    private State currentState;

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
        }
        if (playerRigid == null)
        {
            playerRigid = target.GetComponent<Rigidbody2D>();
        }
        if (rb2d == null)
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
        currentState = State.Idle;
    }

    private void Update()
    {
        if (currentState == State.Idle)
        {
            if (Vector2.Distance(CurPos, Target) < chaseDist)
            {
                currentState = State.Chase;
            }
        }
        else if (currentState == State.Chase)
        {
            if (Vector2.Distance(CurPos, Target) > chaseDist)
            {
                // stop
                CurPos = Vector2.MoveTowards(CurPos, CurPos, movementSpeed * Time.deltaTime);
                currentState = State.Idle;
            }
            else
            {
                if (Physics.Linecast(CurPos, Target))
                {
                    Debug.Log("Blocked");
                }
                CurPos = Vector2.MoveTowards(CurPos, Target, movementSpeed * Time.deltaTime);
            }
        }
        else if (currentState == State.Flee)
        {
            currentState = State.Idle;
            // if player much stronger flee?
        }
        else
        {
            currentState = State.Idle;
        }
    }
}