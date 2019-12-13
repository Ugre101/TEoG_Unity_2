using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    // Public
    public Transform _player;

    public KeyBindings keys;

    public GameObject miniMap, bigMap;
    public RenderTexture miniTexture,bigTexture;
    [Header("Settings")]
    [Range(1f, 10f)]
    public float smoothing = 1f;

    // Private
    private bool mini = true, big = false;

    [SerializeField]
    private Vector3 _offset = new Vector3(1f, 0, -10);

    private Camera cam;
  //  private float _down;

    private enum States
    {
        Hidden,
        Mini,
        Big
    }

    private States curState;

    private States NewState
    {
        get
        {
            if (curState == States.Big)
            {
                curState = States.Hidden;
                return curState;
            } else
            {
                curState++;
                return curState;
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = 80f;
        // add event to change main map.
        curState = States.Mini;
        UpdateMapState(curState);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 _target = _player.transform.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _target, smoothing);
        if (keys.mapKey.GetKeyDown())
        {
            UpdateMapState(NewState);
            //  _down = Time.time;
        }
        #region Old hold down code
        /*  if (Input.GetKeyUp(keys.mapKey))
          {
              if (Time.time - _down > 0.8f)
              {
                  if (miniMap.activeSelf || bigMap.activeSelf)
                  {
                      UpdateMapState(States.Hidden);
                  }
                  else
                  {
                      UpdateMapState(States.Mini);
                  }
              }
              else
              {
                  UpdateMaps();
              }
          }*/
        #endregion
    }
    /*
    private void UpdateMaps()
    {
        cam.orthographicSize = big ? 160f : 80f;
        miniMap.SetActive(mini);
        bigMap.SetActive(big);
    }
    */

    private void UpdateMapState(States state)
    {
        switch (state)
        {
            case States.Big:
                mini = false;
                big = true;
                cam.orthographicSize = 160f;
                cam.targetTexture = bigTexture;
                break;

            case States.Hidden:
                mini = false;
                big = false;
                break;

            case States.Mini:
            default:
                mini = true;
                big = false;
                cam.orthographicSize = 80f;
                cam.targetTexture = miniTexture;
                break;
        }
        miniMap.SetActive(mini);
        bigMap.SetActive(big);
    }
}