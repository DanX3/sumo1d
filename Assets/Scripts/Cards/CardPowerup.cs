using UnityEngine;

public class CardPowerup : Card
{
    [Range(1, 10)] public int durationInTurns = 1;
    int turnsLeft;
    [SerializeField] TMPro.TMP_Text durationLabel;

    private Player target;
    public PowerupWeakness weakness;
    public int weaknessCount = 1;
    int weaknessLeft;


    new void Start()
    {
        base.Start();
        weaknessLeft = weaknessCount;
        durationLabel.text = durationInTurns + "";
        turnsLeft = durationInTurns;
    }

    public override CardType GetCardType()
    {
        return CardType.Powerup;
    }

    public override void Play(Player target)
    {
        this.target = target;
        target.stats.powerups.Add(this);

        base.Play(target);

        target.OnTurnEnd += OnTurnPassed;

        // V   V   V    V   V   V    V   V   V    V   V   V    V   V   V    
        // V                                                           V
        // V                Implement here weaknesses                  V
        // V                                                           V
        // V   V   V    V   V   V    V   V   V    V   V   V    V   V   V    
        switch (weakness)
        {
            case PowerupWeakness.None:
                break;
            case PowerupWeakness.OnDamageReceived:
                target.OnDamageDealt += (_, _) => TickWeakness();
                break;
        }

        user.playedCardsHistory.Add(this);
    }

    private void Remove()
    {
        // Debug.LogWarning($"Powerup: removing {this.cardName}");

        target.OnTurnEnd -= OnTurnPassed;
        target.stats.powerups.Remove(this);
        Discard();
    }

    private void OnTurnPassed()
    {
        // Debug.Log("Turn Passed for " + gameObject.name);
        if (--turnsLeft <= 0)
            Remove();
    }

    private void TickWeakness()
    {
        if (--weaknessLeft <= 0)
            Remove();
    }
}