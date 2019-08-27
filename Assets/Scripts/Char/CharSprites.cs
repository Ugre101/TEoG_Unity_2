using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Char sprites", menuName = "Char sprites")]
public class CharSprites : ScriptableObject
{
    public Sprite humanMale;
    public Sprite humanFemale;
    public Sprite orcMale;
    public Sprite elfMale;
    public Sprite elfFemale;
    public void OnEnable()
    {
        // Nice this works meaning I can use this to know what sprite to save!
        Debug.Log(humanMale.name);
    }
}
