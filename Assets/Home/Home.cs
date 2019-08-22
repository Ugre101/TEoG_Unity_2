using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Home", menuName = ("Home"))]
public class Home : ScriptableObject
{
    public HomeStats Stats = new HomeStats();
}
