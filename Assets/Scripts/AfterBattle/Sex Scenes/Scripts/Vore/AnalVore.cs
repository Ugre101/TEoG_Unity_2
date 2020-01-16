using UnityEngine;
using Vore;

[CreateAssetMenu(fileName = "Anal vore", menuName = ("Sex/Vore/Anal vore"))]
public class AnalVore : VoreScene
{
    public override bool CanDo(BasicChar player, ThePrey Other)
    {
        return player.Vore.Anal.CanVore(Other);
    }

    public override string Vore(PlayerMain player, ThePrey other)
    {
        if (player.Vore.Anal.Vore(other))
        {
            if (true)
            {
                return $"Seeing your foe fall, you eagerly make your way up to them as you unbutton your pants and undergarments. Your foe sighs as they imagine what you’re going to do next. You turn your body around and lower your ass cheeks to their face. They grab your ass with their hands and bring their mouth to your hole, seeing no other option. Your alternative plan starts as you push your ass forcefully against their face. Your foe gasps in surprise as your hole touches their nose and stretches, enveloping their head.\n\n Muffled protests come from your waist as they instinctively push against your cheeks, attempting to free themselves. You squat down and grunt as your rectum pulls hard, forcing your meal up to their chest. As their shoulders get pulled in, their arms can't push against your ass, making it easier to pull them in. Your wince as your food makes its way through your gut, stretching it as you pull them in.\n\n You notice that only their legs are left; you grin and straighten your back, using their limbs as a pseudo-chair. You bounce your hips, hammering them further into your bowels. Your cheeks make contact with the ground as they hungrily shove the last of your foe into your depths. Rough shoves and struggles in your gut are all that is left of them as your gut conforms and kneads its meal.";
            }
            else
            {
                return "As your foe crumbles from your back hooves' last kick, you decide to keep your back turned and slowly back into them." +
                 " Rubbing their head from the final blow, your opponent has only a second to notice your large equine rear descending on them, trapping them in darkness. Rough grunting and the sounds of squeezing accompany this surprise as you lift your foe up to their chest with your twisted strength." +
                 "\n\n Your tail flicks upward sharply with each contraction of your anal muscles, your ass feasting upon the poor soul trapped inside. The impressive display of control you have over your rear continues as your prey suddenly disappears up to their waist into your bowels." +
                 " Pleasurable struggles are given to you from inside your equine half as your conquest twists and pushes your sensitive walls, encouraging you to finish your meal.\n\n Not wanting to disappoint your gut, you make one last effort to envelop your foe with your rectum." +
                 " With immense force, their legs are pulled in, leaving their ankles and feet squeezed harshly by your ass. With a loud \"schluck\" their ankles are pulled in, their feet following close behind." +
                 " A satisfied sigh leaves your mouth, and your ass, as you wiggle your hips in victory over your foe. You head back on your journey, your intestines beginning their work on the fresh meat you've conquered.";
            }
        }
        return $"You can't fit him/her into you rectum";
    }
}