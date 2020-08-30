using UnityEngine;
using Vore;

[CreateAssetMenu(fileName = "OralVore", menuName = ("Sex/Vore/OralVore"))]
public class OralVore : VoreScene
{
    public override bool CanDo(BasicChar player, BasicChar Other)
    {
        return player.Vore.Stomach.CanVore(Other);
    }

    public override string Vore(BasicChar player, BasicChar other)
    {
        if (player.Vore.Stomach.Vore(other))
        {
            float fillPrecent = player.Vore.Stomach.FillPrecent;
            return $"You walk up to your foe with a primal hunger in your abdomen. Your foe is still groggy from the beating you gave them, will fulfill your stomach's desire. You swiftly grab their head with your hands and bring their face to yours. They grunt, expecting a make-out session, only for their eyes to widen as your mouth does the same. You take in their head in one motion and pin their arms to their waist, holding them in place. You lick their face, enjoying their taste, as you lean forward, pushing their head and neck into your greedy throat.\n\n Loud gulping noises can be heard as you stretch your mouth even further and take in their shoulders. Your muscles strain and bulge as you lift your meal off the ground, suspending them in the air, allowing their torso to slide down into your stomach, leaving only their weakly-flailing legs outside. Your stomach bulges as they enter your guts, which give a rumble of approval and anticipation for the rest of its meal. Your hands make their way up to their calves as you grip tightly and give a hard shove, pushing them in to their ankles.\n\nYou open wide and let their feet slide in, your jaws snapping shut as your food is forced to accept its fate. Your filled stomach stretches and heaves as your prey struggles and pushes in futile attempts to free itself.{(fillPrecent > 0.5 ? " You struggle to get back to your feet, your distended stomach sagging heavily with its weight. You wince in discomfort, walking bow-legged for a little to handle its weight." : "")}";
        }
        else
        {
            return "You cannot fit more into your stomach!";
        }
    }
}