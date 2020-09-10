using TMPro;
using UnityEngine;

namespace StartMenuStuff
{
    public class MainTitle : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _title;

        private Color _bottom
            , _top;

        private VertexGradient _gradient;

        private int _counter = 0;
        // Start is called before the first frame update

        private void Start()
        {
            if (_title == null)
            {
                _title = GetComponent<TextMeshProUGUI>();
            }
            _title.enableVertexGradient = true;

            _bottom = new Color(191, 0, 191);
            _top = new Color(0, 0, 191);
            InvokeRepeating(nameof(Gradshift), 0.0f, 1.0f);
        }
        
        private void Gradshift()
        {
            _counter++;
            _title.colorGradient = AStepATime();
            Debug.Log(_counter);
        }

        private VertexGradient AStepATime()
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
}