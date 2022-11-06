public class SetCost0InHand : InstantEffect
{
    public override void Play(Player user)
    {
        user.deckManager.GetRandomCardInHand().SetManaCost(0);
    }
}
