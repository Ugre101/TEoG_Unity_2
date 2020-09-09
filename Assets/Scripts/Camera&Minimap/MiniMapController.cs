using UnityEngine;
using UnityEngine.Serialization;

public class MiniMapController : MonoBehaviour
{
    // Public
    [FormerlySerializedAs("_player")] [SerializeField] private Transform player = null;

    [SerializeField] private GameObject miniMap = null, bigMap = null;

    [SerializeField] private RenderTexture miniTexture = null, bigTexture = null;

    [Header("Settings")]
    [Range(1f, 10f)]
    [SerializeField] private float smoothing = 1f;

    // Private
    private bool mini = true, big = false;

    [FormerlySerializedAs("_offset")] [SerializeField] private Vector3 offset = new Vector3(1f, 0, -10);

    private Camera cam;
    //  private float _down;

    private enum MiniCamStates
    {
        Hidden,
        Mini,
        Big
    }

    private MiniCamStates curState;

    private MiniCamStates NewState
    {
        get
        {
            // TODO check so this statement works
            curState = curState == MiniCamStates.Big ? MiniCamStates.Hidden : curState + 1;
            return curState;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        player = player != null ? player : PlayerSprite.Instance.transform;
        cam = GetComponent<Camera>();
        cam.orthographicSize = 80f;
        // add event to change main map.
        curState = MiniCamStates.Mini;
        UpdateMapState(curState);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 target = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, target, smoothing);
        if (KeyBindings.MapKey.KeyDown)
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

        #endregion Old hold down code
    }

    /*
    private void UpdateMaps()
    {
        cam.orthographicSize = big ? 160f : 80f;
        miniMap.SetActive(mini);
        bigMap.SetActive(big);
    }
    */

    private void UpdateMapState(MiniCamStates state)
    {
        switch (state)
        {
            case MiniCamStates.Big:
                mini = false;
                big = true;
                cam.orthographicSize = 160f;
                cam.targetTexture = bigTexture;
                break;

            case MiniCamStates.Hidden:
                mini = false;
                big = false;
                break;

            case MiniCamStates.Mini:
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