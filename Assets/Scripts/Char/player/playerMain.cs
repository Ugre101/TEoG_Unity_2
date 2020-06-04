public class PlayerMain : BasicChar
{
    // Start is called before the first frame update
    public override void Setup()
    {
        base.Setup();
        Currency.Gold += 100;
    }

    public void PlayerInit(string first, string last)
    {
        Identity.FirstName = first;
        Identity.LastName = last;
    }

    private void SubscribleToEvents()
    {
        RaceSystem.RaceChange += Events.SoloEvents.RaceChange;
    }

    public PlayerMain(Age age, Body body, ExpSystem expSystem, Perks perk, EssenceSystem essence) : base(age, body, expSystem, essence)
    {
    }
}