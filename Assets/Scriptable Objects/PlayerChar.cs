using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "PlayerChar", menuName = "Player")]
public class PlayerChar : ScriptableObject
{
    // Private
    [SerializeField]
    private float hp, wp;
    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            float toChange = Mathf.Clamp(value, -hp, _maxHP._value - hp);
            hp += toChange;
            healthChange();
        }
    }
    public float Wp
    {
        get
        {
            return wp;
        }
        set
        {
            float toChange = Mathf.Clamp(value, -wp, _maxWP._value - wp);
            wp += toChange;
            healthChange();
        }
    }
    [SerializeField]
    private int exp = 0, level = 0, statPoints = 0, perkPoints = 0;

    [SerializeField]
    private CharStats _maxHP, _maxWP;

    // Public
    [SerializeField]
    private CharStats _str;
    public float Str
    {
        get
        {
            return _str._value;
        }
        set
        {
            _str._baseValue += value;
        }
    }
    [SerializeField]
    private CharStats _charm;
    public float Charm
    {
        get
        {
            return _charm._value;
        }
        set
        {
            _charm._baseValue += value;
        }
    }

    public int Exp
    {
        get { return exp; }
        set
        {
            expChange();
            exp += value;
            if (exp >= maxEXP())
            {
                exp -= maxEXP();
                level++;
                statPoints += 3;
                perkPoints++;
                // Eventlog level up;
            }
        }
    }

    private int maxEXP()
    {
        return (int)Mathf.Round(30f * Mathf.Pow(1.05f, level - 1f));
    }

    private void Awake()
    {
    }

    private void OnEnable()
    {
        _str._baseValue = 5f;
        _charm._baseValue = 5f;
        _maxHP._baseValue = 100;
        _maxWP._baseValue = 100;
        hp = _maxHP._value;
        wp = _maxWP._value;
    }

    // Function to gain and lose hp/wp for regereation and combat.
    public void TakeHealthDamage(float damageDealt)
    {
        float dmg = damageDealt;
        hp -= Mathf.Max(0, dmg);
        healthChange();
        if (hp <= 0)
        {
            // Defeat
        }
    }

    public void TakeWillDamage(float damageDealt)
    {
        float dmg = damageDealt;
        wp -= Mathf.Max(0, dmg);
        healthChange();
        if (wp <= 0)
        {
            // Defeat
        }
    }
    // public functions for health/wp/exp bars.
    public float hpSlider()
    {
        return hp / _maxHP._value;
    }

    public string hpStatus()
    {
        return $"{hp}/{_maxHP._value}";
    }

    public float wpSlider()
    {
        return wp / _maxWP._value;
    }

    public string wpStatus()
    {
        return $"{wp}/{_maxWP._value}";
    }

    public float expSlider()
    {
        return (float)exp / (float)maxEXP();
    }

    public string expStatus()
    {
        return $"{exp}/{maxEXP()}";
    }

    public string levelStatus()
    {
        return $"Level: {level}";
    }

    // End
    public delegate void HealthChange();

    public static event HealthChange healthChange;

    public void manualSliderUpdate()
    {
        healthChange();
    }

    public delegate void ExpChange();

    public static event ExpChange expChange;

    public void manualExpUpdate()
    {
        expChange();
    }
}