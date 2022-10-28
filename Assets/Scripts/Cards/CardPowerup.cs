
public class CardPowerup : Card
{
    public int durationInTurns;

    public override CardType GetCardType()
    {
        return CardType.Powerup;
    }

    public override void Play(Player targets)
    {
        base.Play(targets);

        // TODO: apply powerup to player
    }

    private void OnTurnPassed()
    {
        // TODO: agganciare a evento di fine turno 
        durationInTurns--;

        if (durationInTurns == 0)
            Discard();
    }
}