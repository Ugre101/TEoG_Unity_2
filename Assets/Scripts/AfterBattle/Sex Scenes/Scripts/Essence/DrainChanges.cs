using UnityEngine;

/// <summary>
///  Remember to constuct this before essence change
/// </summary>
public class DrainChangeHandler
{
    private readonly SexualOrgans playerOldOrgans;
    private readonly SexualOrgans otherOldOrgans;
    private readonly BasicChar player;
    private readonly BasicChar other;

    public DrainChangeHandler(BasicChar player, BasicChar other)
    {
        this.player = player;
        playerOldOrgans = UgreTools.JsonClone(player.SexualOrgans);
        this.other = other;
        otherOldOrgans = UgreTools.JsonClone(other.SexualOrgans);
    }

    public string BothChanges => DrainChanges.BothChanges(player, other, playerOldOrgans, otherOldOrgans);
}

public static class DrainChanges
{
    public static string BothChanges(BasicChar player, BasicChar enemy, SexualOrgans old, SexualOrgans eold) => PlayerDrainChanges(player, old) + "\n" + EnemyDrainChanges(enemy, eold);

    public static string EnemyDrainChanges(BasicChar enemy, SexualOrgans eold)
    {
        SexualOrgans ecurrent = enemy.SexualOrgans;
        string b = string.Empty;
        if (eold.Dicks.List.Count > ecurrent.Dicks.List.Count)
        {
            b += ecurrent.Dicks.List.Count < 1 ? $"You see their dick shrinking completely into their body, turning them into a {enemy.Gender()}." : "They lost a dick";
        }
        else if (eold.Dicks.List.Count < ecurrent.Dicks.List.Count)
        {
            b += "They have grown a dick";
        }
        else if (ecurrent.Dicks.List.Count > 0)
        {
            for (int e = 0; e < eold.Dicks.List.Count; e++)
            {
                if (Mathf.Round(eold.Dicks.List[e].BaseSize) > Mathf.Round(ecurrent.Dicks.List[e].BaseSize))
                {
                    b = "You see their dick shrinking"; //Need to add what dick e.g. their second dick shrinks etc
                }
                else if (Mathf.Round(eold.Dicks.List[e].BaseSize) > Mathf.Round(ecurrent.Dicks.List[e].BaseSize))
                {
                    b = "You see their dick growing";
                }
            }
        }

        if (eold.Balls.List.Count > ecurrent.Balls.List.Count)
        {
            b += ecurrent.Balls.List.Count < 1 ? "\nThey lost a pair of balls" : "\nThey lost a pair of balls";
        }
        else if (eold.Balls.List.Count < ecurrent.Balls.List.Count)
        {
            b += "\nThey have grown a pair of balls";
        }
        else if (ecurrent.Balls.List.Count > 0)
        {
            for (int e = 0; e < ecurrent.Balls.List.Count; e++)
            {
                if (Mathf.Round(eold.Balls.List[e].BaseSize) > Mathf.Round(ecurrent.Balls.List[e].BaseSize))
                {
                    b += "\nYou see their balls shrinking";
                }
                else if (Mathf.Round(eold.Balls.List[e].BaseSize) < Mathf.Round(ecurrent.Balls.List[e].BaseSize))
                {
                    b += "\nYou see their balls growing";
                }
            }
        }

        if (eold.Boobs.List.Count > ecurrent.Boobs.List.Count)
        {
            b += "\nThey loost a pair of breasts";
        }
        else if (eold.Boobs.List.Count < ecurrent.Boobs.List.Count)
        {
            b += "\nThey have grown a pair of breasts";
        }
        else
        {
            for (int e = 0; e < eold.Boobs.List.Count; e++)
            {
                if (Mathf.Round(eold.Boobs.List[e].BaseSize) > Mathf.Round(ecurrent.Boobs.List[e].BaseSize))
                {
                    b += "\nYou see their breasts shrinking.";
                }
                else if (Mathf.Round(eold.Boobs.List[e].BaseSize) < Mathf.Round(ecurrent.Boobs.List[e].BaseSize))
                {
                    b += "\nYou see their breasts growing.";
                }
            }
        }

        if (eold.Vaginas.List.Count > ecurrent.Vaginas.List.Count)
        {
            b += ecurrent.Vaginas.List.Count < 1 ? $"\nYou see their pussy closing completely and disappear, turning them into a {enemy.Gender()}." : "\nThey a lost a pussy";
        }
        else if (eold.Vaginas.List.Count < ecurrent.Vaginas.List.Count)
        {
            b += "\nThey a have grown a pussy";
        }
        else if (ecurrent.Vaginas.List.Count > 0)
        {
            for (int e = 0; e < eold.Vaginas.List.Count; e++)
            {
                if (Mathf.Round(eold.Vaginas.List[e].BaseSize) > Mathf.Round(ecurrent.Vaginas.List[e].BaseSize))
                {
                    b += "You feel their pussy tightening";
                }
                else if (Mathf.Round(eold.Vaginas.List[e].BaseSize) < Mathf.Round(ecurrent.Vaginas.List[e].BaseSize))
                {
                    b += "You feel their pussy growing"; //Need better word/phrase than growing
                }
            }
        }
        return b;
    }

    public static string PlayerDrainChanges(BasicChar player, SexualOrgans old)
    {
        string a = string.Empty;
        SexualOrgans current = player.SexualOrgans;
        if (old.Dicks.List.Count < current.Dicks.List.Count)
        {
            a = "You have grown a dick!";
        }
        else if (current.Dicks.List.Count < old.Dicks.List.Count)
        {
            a = current.Dicks.List.Count < 1 ? $"You feel your dick shrinking completely into your body, turning you into a {player.Gender()}." : "You have lost a dick";
        }
        else if (current.Dicks.List.Count > 0)
        {
            for (var e = 0; e < current.Dicks.List.Count; e++)
            {
                if (Mathf.Round(old.Dicks.List[e].BaseSize) < Mathf.Round(current.Dicks.List[e].BaseSize))
                {
                    a += "\nYou feel your dick growing.";
                }
                else if (Mathf.Round(current.Dicks.List[e].BaseSize) < Mathf.Round(old.Dicks.List[e].BaseSize))
                {
                    a += "\nYou feel your dick shrinking.";
                }
            }
        }

        if (old.Balls.List.Count < current.Balls.List.Count)
        {
            a += "\nyou have grown a pair of balls";
        }
        else if (current.Balls.List.Count < old.Balls.List.Count)
        {
            a += current.Balls.List.Count < 1 ? $"Your balls shrink into your body until " : "\nyou have lost a pair of balls";
        }
        else if (current.Balls.List.Count > 0)
        {
            for (var e = 0; e < current.Balls.List.Count; e++)
            {
                if (Mathf.Round(old.Balls.List[e].BaseSize) < Mathf.Round(current.Balls.List[e].BaseSize))
                {
                    a += "\nyou feel you balls growing";
                }
                else if (Mathf.Round(current.Balls.List[e].BaseSize) < Mathf.Round(old.Balls.List[e].BaseSize))
                {
                    a += "\nyou feel you balls shrinking";
                }
            }
        }

        if (old.Boobs.List.Count < current.Boobs.List.Count)
        {
            a += "\nYou have grown a new pair of breasts.";
        }
        else if (current.Boobs.List.Count < old.Boobs.List.Count)
        {
            a += "\nYou have lost a pair of breasts";
        }
        else if (current.Boobs.List.Count > 0)
        {
            for (var e = 0; e < current.Boobs.List.Count; e++)
            {
                if (Mathf.Round(old.Boobs.List[e].BaseSize) < Mathf.Round(current.Boobs.List[e].BaseSize))
                {
                    a += "\nYou feel your breasts grow bigger.";
                }
                else if (Mathf.Round(current.Boobs.List[e].BaseSize) < Mathf.Round(old.Boobs.List[e].BaseSize))
                {
                    a += "\nYou feel your breasts shrinking.";
                }
            }
        }

        if (old.Vaginas.List.Count < current.Vaginas.List.Count)
        {
            a += "\nYou gave grown a pussy";
        }
        else if (current.Vaginas.List.Count < old.Vaginas.List.Count)
        {
            a += "\nYou have lost a pussy";
        }
        else if (current.Vaginas.List.Count > 0)
        {
            for (var e = 0; e < current.Vaginas.List.Count; e++)
            {
                if (Mathf.Round(old.Vaginas.List[e].BaseSize) < Mathf.Round(current.Vaginas.List[e].BaseSize))
                {
                    a += "\nYou feel your pussy grow";
                }
                else if (Mathf.Round(current.Vaginas.List[e].BaseSize) < Mathf.Round(old.Vaginas.List[e].BaseSize))
                {
                    a += "\nYou feel your pussy tighten";
                }
            }
        }
        /*
        if (CheckGender(old) != CheckGender(current))
        {
            a += "\n\nYou have become a " + Pronoun(CheckGender(current)) + "!\n";
        }*/
        return a;
    }
}