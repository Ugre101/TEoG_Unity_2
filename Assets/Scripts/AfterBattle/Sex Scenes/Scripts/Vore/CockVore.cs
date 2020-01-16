using UnityEngine;

[CreateAssetMenu(fileName = "Cock vore", menuName = ("Sex/Vore/Cock vore"))]
public class CockVore : VoreScene
{
    public override bool CanDo(BasicChar player, Vore.ThePrey Other)
    {
        if (player.SexualOrgans.Balls.Count < 1)
        {
            return false;
        }
        else if (player.SexualOrgans.Dicks.Count < 1)
        {
            return false;
        }
        return player.Vore.Balls.CanVore(Other);
    }

    public override string Vore(PlayerMain player, Vore.ThePrey other)
    {
        if (player.Vore.Balls.Vore(other))
        {
            if (true)
            {
                return "Your confidently stride up to your opponent, looking at your next meal's form as your reach for your waist. They bend their head forward, and aren't surprised when they are greeted with your erect cock. They open their mouth to start sucking, only for your dick to stretch wide as well. Before they have time to react, you thrust forward, quickly enveloping their head within your dick. You use your hands to squeeze the newest bulge in your member as you eagerly thrust around your foe, slowly forcing them deeper with each hump.\n\nYour breathing deepens as their shoulders begin their journey to your sac, stretching your dick wide to accommodate its food. Both of your hands reach around your massively-distended dick as you rub over the bulge its food is making. Your sac churns audibly with hunger for its meal, salivating precum to speed up your foe's descent. Your massaging actions causes squelching sounds as your now-massive member greedily sucks in their waist. Having recovered slightly from their initial shock they frantically wiggle, causing their body to twist and squirm within your member, their struggles bringing you waves of pleasure as you shiver from their protests.\n\nYou lift your cock with both hands, giving it long, slow strokes to speed up its meal. You sigh, giving soft humps as all that remains of your foe is a bulge in your shaft, sliding down. Loud sloshing sounds can be heard as your cock's recent meal ends its journey and is deposited in your sac. Your nuts heave and drag against the floor as you haul your soon-to-be load off with you.";
            }
            // Both hands; your enemy's too heavy for one arm still
            else
            {
                return "Seeing your opponent lying there defeated gives you an idea; your cock stiffening in anticipation as you make your way to your soon-to-be prey. Your foe looks up from their position and jumps from your large equine form blocking out the sun." +
                 " Standing over your foe you convey your desires as your cock makes a loud 'thwap' against your stomach. Your opponent gets the idea, slowly approaching your shaft(s) and hesitantly stretching their hands forward." +
                 "<br><br> Sensing their hesitation, you made a deafening stomp with your hooves, causing your foe to quickly stroke your dick. You pull your hips back away from your foe, confusing their expectations - they thought you wanted a simple blowjob." +
                 " An excited whip of your tail and a small grunt of dominance are the only signs your prey receive - you make a single thrust at your opponent’s head, your equine shaft hungrily grabbing them up to their chest." +
                 " You instinctively snort as you claim your foe with your cock, its muscles sucking on their form, as if tasting them. Desperate for more, you flex your groin with immense strength and pull your prey in up to their crotch." +
                 "<br><br> Gasps of nerve-wracking pleasure escape you as your prey begins its fruitless struggles inside your monstrous shaft. Not wanting to risk an early orgasm (and releasing them), you repeatedly flex your cock, causing it to beat your meal into submission against your stomach." +
                 " Pleased with your display of power over your meal, you pull in their legs with little effort and seal your cock head around their feet. You sigh in relief as you feel the rest of your prey deposited in your sac. Feeling sated, you make your way back to the road, your nuts sagging heavily between your legs.";
            }
        }
        return "You can't fit any more into your balls!";
    }
}