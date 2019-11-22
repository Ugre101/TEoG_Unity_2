using UnityEngine;

[CreateAssetMenu(fileName = "Home", menuName = ("Home"))]
public class Home : ScriptableObject
{
    [SerializeField]
    private HomeStats stats = new HomeStats();

    public HomeStats Stats => stats;
}