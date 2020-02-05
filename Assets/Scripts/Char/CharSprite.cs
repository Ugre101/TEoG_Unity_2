using UnityEngine;

[CreateAssetMenu(fileName = "Char sprites", menuName = "Char/CharSprites/Char sprite")]
public class CharSprite : ScriptableObject
{
    [SerializeField] private Genders gender;
    [SerializeField] private Races race;
    [SerializeField] private ClassTypes classType;
    [SerializeField] private GenderTypes genderType;
    [SerializeField] private Sprite sprite;

    public Genders Gender => gender;
    public Races Race => race;
    public Sprite Sprite => sprite;
    public ClassTypes ClassType => classType;
    public GenderTypes GenderType => genderType;
}