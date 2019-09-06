using UnityEngine;

[CreateAssetMenu(fileName = "Char sprites", menuName = "Char/CharSprites/Char sprite")]
public class CharSprite : ScriptableObject
{
    public Genders gender;
    public Races race;
    public Sprite sprite;
    public ClassTypes classType;
}