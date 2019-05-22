using UnityEngine;
public class MiniMapController : MonoBehaviour
{
    // Public
    public Transform _player;
    public KeyBindings keys;

    public GameObject miniMap, bigMap;

    // Private
    private bool mini = true, big = false;

    private Vector3 _offset = new Vector3(1f, 0, -10);
    private Camera cam;
    private float _smoothing = 1f;
    private float _down;
    // Start is called before the first frame update
    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = 80f;
        // add event to change main map.
        miniMap.SetActive(mini);
        bigMap.SetActive(big);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 _target = _player.transform.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _target, _smoothing);
        if (Input.GetKeyDown(keys.mapKey))
        {
            _down = Time.time;
        }
        if (Input.GetKeyUp(keys.mapKey))
        {
            if (Time.time - _down > 0.8f)
            {
                if (miniMap.activeSelf || bigMap.activeSelf)
                {
                    miniMap.SetActive(false);
                    bigMap.SetActive(false);
                }else
                {
                    miniMap.SetActive(true);
                    bigMap.SetActive(false);
                }
            }
            else
            {
                UpdateMaps();
            }
        }
    }

    private void UpdateMaps()
    {
        mini = !mini;
        big = !big;
        cam.orthographicSize = big ? 160f : 80f;
        miniMap.SetActive(mini);
        bigMap.SetActive(big);
    }
}