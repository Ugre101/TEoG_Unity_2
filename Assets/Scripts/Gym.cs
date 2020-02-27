public class Gym : Building
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void Train(BasicChar basicChar)
    {
        float muscleGain = 0, fatLost = 0;
        // TODO limit muscle gain based on str
        basicChar.Body.Muscle.GainFlat(muscleGain);
        basicChar.Body.Fat.LoseFlat(fatLost);
    }
}