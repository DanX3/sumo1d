
public class CardPowerup : Card
{
    public int durationInTurns;

    public override CardType GetCardType()
    {
        return CardType.Powerup;
    }

    public override void Play(Player target)
    {
        base.Play(target);

        // TODO: apply powerup to player
        target.OnTurnEnd += OnTurnPassed;
    }

    private void OnTurnPassed()
    {
        // TODO: agganciare a evento di fine turno 
        durationInTurns--;

        if (durationInTurns == 0)
            Discard();
    }
}