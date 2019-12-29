using System.Collections.Generic;
using UnityEngine;

public static class RandomName
{
    private static readonly List<string> maleNames = new List<string> {"Jerome", "Napoleon", "Duncan", "Brant", "Chance", "Dewitt", "Brendan",
        "Asim", "Faith", "Macy", "Landon", "Sulaiman", "Iestyn", "Gordon", "Hector", "Haris", "Lee", "Simran", "Ronnie", "Rishi",
        "Bartosz", "Shelley", "Virgil", "Howard", "Rio", "Prince", "Glenn", "Daniel", "Felipe", "Willie", "Aden", "Brennen",
        "Cale", "Arnav", "Quentin", "River", "Clarence", "Jamal", "Luca", "Brent", "Tyrone", "Ryan", "Damien", "Carmelo",
        "Reese", "Braiden", "Beckham" };

    public static string MaleName => maleNames.Count > 0 ? maleNames[Random.Range(0, maleNames.Count)] : "";

    private static List<string> femaleNames = new List<string> {"Jaiden", "Judy", "Nia", "Kelis", "Chelsea", "Amani", "Veronica", "Kyra",
        "Lauryn", "Alicja", "Tate", "Colleen", "Melody", "Pippa", "Keziah", "Melissa", "Lana", "Marie", "Molly", "Sandra",
        "Dannielle", "Yusra", "Laiba", "Gabrielle", "Syeda", "Amirah", "Lindsay", "Karly", "Itzel", "Clarissa", "Ansley",
        "Leanna", "Briley", "Cara", "Katelynn", "Susan", "Alexis", "Kaia", "Marlee", "Emmy", "Genevieve", "Melany", "Jaylynn",
        "Amari", "Sharon", "Miah", "Karen", "Kylie"
    };

    public static string FemaleName => femaleNames.Count > 0 ? femaleNames[Random.Range(0, femaleNames.Count)] : "";

    private static readonly List<string> lastNames = new List<string> {"Paine", "Ward", "Bostock", "Devine", "Heath", "Bone", "Dupont", "Patterson",
        "Garza", "Stein", "Madden", "Francis", "Villanueva", "Perry", "Lyssa", "Beach", "Crouch", "Sharp", "Clifford", "Wade",
        "Vargas", "Hatfield", "Mata", "Lozano", "Everett", "Krueger", "Jimenez", "Fitzpatrick", "Nelson", "Scott", "Vaughn",
        "Lee", "Hodge", "Blackburn", "Wall", "Hernandez", "Valdez", "Summers", "Mercado", "Villarreal", "Mitchell", "Duran",
        "David", "Black", "Hopkins", "Hughes", "Rangel" };

    public static string LastName => lastNames.Count > 0 ? lastNames[Random.Range(0, lastNames.Count)] : "";

    private static readonly List<string> evilMaleNames = new List<string> {"Neclord", "Virion", "Dario", "Grumio", "Auron", "Jaymes", "Fark", "Cidolfus",
        "Bartholomew", "Arthur"};

    public static string EvilMale => evilMaleNames.Count > 0 ? evilMaleNames[Random.Range(0, evilMaleNames.Count)] : "";

    private static readonly List<string> evilFemaleNames = new List<string> {"Autumn", "Imeena", "Margorie", "Draven", "Lauden", "Ethel", "Cat", "Raven",
        "Senka", "Jinx"};

    private static readonly List<string> evilLastNames = new List<string> {"Crimson", "Kane", "Duke", "Interfector", "Geulimja", "Ebonywood", "Grove",
        "Helion", "Church", "Geulimja", "Moonfall", "Winter", "Hart", "Calarook", "Crypt", "Wolf", "Rex", "Fadington", "Maganti",
        "Hook"};

    private static readonly List<string> dragonMaleNames = new List<string> {"Kalzreot", "Pendryss", "Xierdiss", "Frayvrag", "Cyddrunth", "Zergyr",
        "Cennir", "Akosdiat", "Jygoda", "Qothasdyr", "Bryrgusdirth", "Frorvedeg", "Kytidum", "Jialdrum", "Pegis", "Ymrirth",
        "Dayddrog", "Byrvog", "Brenneg", "Kairranth" };

    private static readonly List<string> dragonFemaleNames = new List<string> {"Adhonth", "Rindyth", "Hendro", "Frarlei", "Fukyss", "Fover", "Chaghess",
        "Indarrass", "Aeghentanth", "Dodhisser", "Nisses", "Rephes", "Eny", "Qierleo", "Qirass", "Zevno", "Persoan",
        "Frairmossolth", "Bayzassath", "Pullontinth" };

    private static readonly List<string> dragonLastNames = new List<string> { "" };
}