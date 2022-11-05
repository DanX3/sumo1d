public class RedrawHand : InstantEffect
{
    public override void Play(Player user)
    {
        int cardCount = user.deckManager.handCount;
        user.deckManager.DiscardHand();
        user.deckManager.Draw(cardCount);
    }
}
