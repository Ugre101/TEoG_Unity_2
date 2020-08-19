public class PlayerMain : BasicChar
{
    public void PlayerInit(string first, string last)
    {
        Identity.SetFirstName(first);
        Identity.SetLastName(last);
    }

    private void SubscribleToEvents()
    {
        RaceSystem.RaceChange += Events.SoloEvents.RaceChange;
    }

    public PlayerMain(Age age, Body body, ExpSystem expSystem, Perks perk) : base(age, body, expSystem)
    {
    }

    public string PlayerID => identity.Id;
}