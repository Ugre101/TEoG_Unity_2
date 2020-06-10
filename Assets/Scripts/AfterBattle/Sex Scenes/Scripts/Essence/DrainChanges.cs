using UnityEngine;
/// <summary>
///  Remember to constuct this before essence change
/// </summary>
public class DrainChangeHandler
{
    private Organs playerOldOrgans, otherOldOrgans;
    private BasicChar player, other;

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
    public static string BothChanges(BasicChar player, BasicChar enemy, Organs old, Organs eold) => PlayerDrainChanges(player, old) + "\n" + EnemyDrainChanges(enemy, eold);

    public static string EnemyDrainChanges(BasicChar enemy, Organs eold)
    {
        Organs ecurrent = enemy.SexualOrgans;
        string b = string.Empty;
        if (eold.Dicks.Count > ecurrent.Dicks.Count)
        {
            b += ecurrent.Dicks.Count < 1 ? $"You see their dick shrinking completely into their body, turning them into a {enemy.Gender()}." : "They lost a dick";
        }
        else if (eold.Dicks.Count < ecurrent.Dicks.Count)
        {
            b += "They have grown a dick";
        }
        else if (ecurrent.Dicks.Count > 0)
        {
            for (int e = 0; e < eold.Dicks.Count; e++)
            {
                if (Mathf.Round(eold.Dicks[e].Size) > Mathf.Round(ecurrent.Dicks[e].Size))
                {
                    b = "You see their dick shrinking"; //Need to add what dick e.g. their second dick shrinks etc
                }
                else if (Mathf.Round(eold.Dicks[e].Size) > Mathf.Round(ecurrent.Dicks[e].Size))
                {
                    b = "You see their dick growing";
                }
            }
        }

        if (eold.Balls.Count > ecurrent.Balls.Count)
        {
            b += ecurrent.Balls.Count < 1 ? "\nThey lost a pair of balls" : "\nThey lost a pair of balls";
        }
        else if (eold.Balls.Count < ecurrent.Balls.Count)
        {
            b += "\nThey have grown a pair of balls";
        }
        else if (ecurrent.Balls.Count > 0)
        {
            for (int e = 0; e < ecurrent.Balls.Count; e++)
            {
                if (Mathf.Round(eold.Balls[e].Size) > Mathf.Round(ecurrent.Balls[e].Size))
                {
                    b += "\nYou see their balls shrinking";
                }
                else if (Mathf.Round(eold.Balls[e].Size) < Mathf.Round(ecurrent.Balls[e].Size))
                {
                    b += "\nYou see their balls growing";
                }
            }
        }

        if (eold.Boobs.Count > ecurrent.Boobs.Count)
        {
            b += "\nThey loost a pair of breasts";
        }
        else if (eold.Boobs.Count < ecurrent.Boobs.Count)
        {
            b += "\nThey have grown a pair of breasts";
        }
        else
        {
            for (int e = 0; e < eold.Boobs.Count; e++)
            {
                if (Mathf.Round(eold.Boobs[e].Size) > Mathf.Round(ecurrent.Boobs[e].Size))
                {
                    b += "\nYou see their breasts shrinking.";
                }
                else if (Mathf.Round(eold.Boobs[e].Size) < Mathf.Round(ecurrent.Boobs[e].Size))
                {
                    b += "\nYou see their breasts growing.";
                }
            }
        }

        if (eold.Vaginas.Count > ecurrent.Vaginas.Count)
        {
            b += ecurrent.Vaginas.Count < 1 ? $"\nYou see their pussy closing completely and disappear, turning them into a {enemy.Gender()}." : "\nThey a lost a pussy";
        }
        else if (eold.Vaginas.Count < ecurrent.Vaginas.Count)
        {
            b += "\nThey a have grown a pussy";
        }
        else if (ecurrent.Vaginas.Count > 0)
        {
            for (int e = 0; e < eold.Vaginas.Count; e++)
            {
                if (Mathf.Round(eold.Vaginas[e].Size) > Mathf.Round(ecurrent.Vaginas[e].Size))
                {
                    b += "You feel their pussy tightening";
                }
                else if (Mathf.Round(eold.Vaginas[e].Size) < Mathf.Round(ecurrent.Vaginas[e].Size))
                {
                    b += "You feel their pussy growing"; //Need better word/phrase than growing
                }
            }
        }
        return b;
    }

    public static string PlayerDrainChanges(BasicChar player, Organs old)
    {
        string a = string.Empty;
        Organs current = player.SexualOrgans;
        if (old.Dicks.Count < current.Dicks.Count)
        {
            a = "You have grown a dick!";
        }
        else if (current.Dicks.Count < old.Dicks.Count)
        {
            a = current.Dicks.Count < 1 ? $"You feel your dick shrinking completely into your body, turning you into a {player.Gender()}." : "You have lost a dick";
        }
        else if (current.Dicks.Count > 0)
        {
            for (var e = 0; e < current.Dicks.Count; e++)
            {
                if (Mathf.Round(old.Dicks[e].Size) < Mathf.Round(current.Dicks[e].Size))
                {
                    a += "\nYou feel your dick growing.";
                }
                else if (Mathf.Round(current.Dicks[e].Size) < Mathf.Round(old.Dicks[e].Size))
                {
                    a += "\nYou feel your dick shrinking.";
                }
            }
        }

        if (old.Balls.Count < current.Balls.Count)
        {
            a += "\nyou have grown a pair of balls";
        }
        else if (current.Balls.Count < old.Balls.Count)
        {
            a += current.Balls.Count < 1 ? $"Your balls shrink into your body until " : "\nyou have lost a pair of balls";
        }
        else if (current.Balls.Count > 0)
        {
            for (var e = 0; e < current.Balls.Count; e++)
            {
                if (Mathf.Round(old.Balls[e].Size) < Mathf.Round(current.Balls[e].Size))
                {
                    a += "\nyou feel you balls growing";
                }
                else if (Mathf.Round(current.Balls[e].Size) < Mathf.Round(old.Balls[e].Size))
                {
                    a += "\nyou feel you balls shrinking";
                }
            }
        }

        if (old.Boobs.Count < current.Boobs.Count)
        {
            a += "\nYou have grown a new pair of breasts.";
        }
        else if (current.Boobs.Count < old.Boobs.Count)
        {
            a += "\nYou have lost a pair of breasts";
        }
        else if (current.Boobs.Count > 0)
        {
            for (var e = 0; e < current.Boobs.Count; e++)
            {
                if (Mathf.Round(old.Boobs[e].Size) < Mathf.Round(current.Boobs[e].Size))
                {
                    a += "\nYou feel your breasts grow bigger.";
                }
                else if (Mathf.Round(current.Boobs[e].Size) < Mathf.Round(old.Boobs[e].Size))
                {
                    a += "\nYou feel your breasts shrinking.";
                }
            }
        }

        if (old.Vaginas.Count < current.Vaginas.Count)
        {
            a += "\nYou gave grown a pussy";
        }
        else if (current.Vaginas.Count < old.Vaginas.Count)
        {
            a += "\nYou have lost a pussy";
        }
        else if (current.Vaginas.Count > 0)
        {
            for (var e = 0; e < current.Vaginas.Count; e++)
            {
                if (Mathf.Round(old.Vaginas[e].Size) < Mathf.Round(current.Vaginas[e].Size))
                {
                    a += "\nYou feel your pussy grow";
                }
                else if (Mathf.Round(current.Vaginas[e].Size) < Mathf.Round(old.Vaginas[e].Size))
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