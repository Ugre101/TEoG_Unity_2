using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddDick : MonoBehaviour
{
    public playerMain _player;
    private Button _btn;
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    private void Start()
    {
        AddDick addDick = this;
        _btn = addDick.GetComponent<Button>();
        _text = addDick.GetComponentInChildren<TextMeshProUGUI>();
        if (_text != null)
        {
            _text.text = $"Add dick: {GetCost()}";
        }
        if (_btn != null)
        {
            _btn.onClick.AddListener(GrowDick);
        }
    }

    private float GetCost()
    {
        float cost = Mathf.Round(30 * Mathf.Pow(3, _player.Dicks.Count));
        return cost;
    }

    private void GrowDick()
    {
        if (_player.Masc.Amount > GetCost())
        {
            _player.Masc.Lose(GetCost());
            _player.AddDick();
            _text.text = $"Add dick: {GetCost()}";
        }
    }
}