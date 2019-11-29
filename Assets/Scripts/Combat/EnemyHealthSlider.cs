using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSlider : BasicSlider
{
    public void Init(BasicChar enemy)
    {
        basicChar = enemy;
        Health.UpdateSliderEvent += changeHealth;
        basicChar.HP.ManualSliderUpdate();
    }
    private void OnDisable()
    {

                Health.UpdateSliderEvent -= changeHealth;

    }

    // Update is called once per frame

    private void changeHealth()
    {
        slider.value = basicChar.HP.SliderValue;
        if (TextMesh != null)
        {
            TextMesh.text = basicChar.HP.Status;
        }
    }
}
