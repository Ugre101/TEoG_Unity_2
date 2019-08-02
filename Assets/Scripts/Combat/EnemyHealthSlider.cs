using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSlider : BasicSlider
{
    public void Init(BasicChar enemy)
    {
        basicChar = enemy;
        Health.updateSlider += changeHealth;
        basicChar.HP.manualSliderUpdate();
    }
    private void OnDisable()
    {

                Health.updateSlider -= changeHealth;

    }

    // Update is called once per frame

    private void changeHealth()
    {
        slider.value = basicChar.HP.Slider();
        if (TextMesh != null)
        {
            TextMesh.text = basicChar.HP.Status();
        }
    }
}
