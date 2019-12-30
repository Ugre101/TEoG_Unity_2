using UnityEngine;

[CreateAssetMenu(fileName = "Char sprites", menuName = "Char/CharSprites/Char sprite")]
public class CharSprite : ScriptableObject
{
    [field: SerializeField] public Genders Gender { get; private set; }
    [field: SerializeField] public Races Race { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public ClassTypes ClassType { get; private set; }
    [field: SerializeField] public GenderTypes GenderType { get; private set; }
}