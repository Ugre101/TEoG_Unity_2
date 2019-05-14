using TMPro;
using UnityEngine;

public class MainTitle : MonoBehaviour
{
    private TextMeshProUGUI _title;

    private Color _bottom
        , _top;

    private VertexGradient _gradient;

    private int _counter = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        _title = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();
        _title.enableVertexGradient = true;
        // StartCoroutine(GradientTimer());
    }

    private void Start()
    {
        _bottom = new Color(191, 0, 191);
        _top = new Color(0, 0, 191);
        InvokeRepeating("gradshift", 0f, 2f);
    }

    private void gradshift()
    {
        _counter++;
        _title.colorGradient = aStepaTime();
    }

    private VertexGradient aStepaTime()
    {
        switch (_counter)
        {
            case 1:
                return new VertexGradient(_top, _top, _top, _top);
            case 2:
                return new VertexGradient(_bottom, _top, _top, _top);
            case 3:
                return new VertexGradient(_bottom, _bottom, _top, _top);
            case 4:
                return new VertexGradient(_bottom, _bottom, _bottom, _top);
            case 5:
                return new VertexGradient(_bottom, _bottom, _bottom, _bottom);
            default:
                _counter = 0;
                return new VertexGradient(_top, _top, _top, _top);
        }
    }
}