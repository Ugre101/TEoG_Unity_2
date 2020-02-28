using UnityEngine;

[CreateAssetMenu(fileName = "Char sprites", menuName = "Char/CharSprites/Char sprite")]
public class CharSprite : ScriptableObject
{
    [SerializeField] private Genders gender = Genders.Cuntboy;
    [SerializeField] private Races race = Races.Amazon;
    [SerializeField] private ClassTypes classType = ClassTypes.Healer;
    [SerializeField] private GenderTypes genderType = GenderTypes.Feminine;
    [SerializeField] private Sprite sprite = null;

    [Header("1 is standard")]
    [Range(0.1f, 2f)]
    [SerializeField] private float heightOfSprite = 1f;

    public Genders Gender => gender;
    public Races Race => race;
    public Sprite Sprite => sprite;
    public ClassTypes ClassType => classType;
    public GenderTypes GenderType => genderType;
    public float HeightOfSprite => heightOfSprite;
}