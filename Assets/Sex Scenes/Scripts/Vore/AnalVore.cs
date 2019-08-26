using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Anal vore", menuName = ("Sex/Vore/Anal vore"))]
public class AnalVore : VoreScene
{
    public override string StartScene(playerMain player, BasicChar other)
    {
        if (player.Vore.Anal.Vore(other))
        {
            return $"Seeing your foe fall, you eagerly make your way up to them as you unbutton your pants and undergarments. Your foe sighs as they imagine what you’re going to do next. " +
                 "You turn your body around and lower your ass cheeks to their face. They grab your ass with their hands and bring their mouth to your hole, seeing no other option. Your alternative plan starts as you push your ass forcefully against their face." +
                 " Your foe gasps in surprise as your hole touches their nose and stretches, enveloping their head.\n\n Muffled protests come from your waist as they instinctively push against your cheeks, attempting to free themselves." +
                 " You squat down and grunt as your rectum pulls hard, forcing your meal up to their chest. As their shoulders get pulled in, their arms can't push against your ass, making it easier to pull them in." +
                 " Your wince as your food makes its way through your gut, stretching it as you pull them in.\n\n You notice that only their legs are left; you grin and straighten your back, using their limbs as a pseudo-chair." +
                 " You bounce your hips, hammering them further into your bowels. Your cheeks make contact with the ground as they hungrily shove the last of your foe into your depths." +
                 " Rough shoves and struggles in your gut are all that is left of them as your gut conforms and kneads its meal.";
            ;
        }
       else
        {
            // can't fit or too full?
            return $"You can't fit him/her into you rectum";
        }
    }
}
