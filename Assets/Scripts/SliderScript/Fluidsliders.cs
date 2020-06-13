using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class FluidSliders : MonoBehaviour
{
    [SerializeField] protected PlayerHolder playerHolder = null;
    private PlayerMain setPlayer;

    protected PlayerMain Player
    {
        get
        {
            if (setPlayer == null)
            {
                playerHolder = playerHolder != null ? playerHolder : PlayerHolder.GetPlayerHolder;
                if (playerHolder.BasicChar is PlayerMain p)
                {
                    setPlayer = p;
                }
            }
            return setPlayer;
        }
    }

    [SerializeField] protected TextMeshProUGUI statusText = null;

    [SerializeField] protected Slider slider = null;

    protected void Start()
    {
        slider = slider != null ? slider : GetComponent<Slider>();
        Setup();
    }

    public virtual void Setup()
    {
    }
}