public class Stats
{
    public int power;
    public int spirit;
    public int weight;
    public int reflexes;
    public int critical;
    public int damageAdd;
    public float damageMul;
    public float critAdd;
    public float critMul;
    public int handCount;
    public int arena;
    public int damageThreshold;

    public Stats(int power, int spirit, int weight, int reflex,
        int critical, int damageAdd = 0, float damageMul = 1, float critAdd = 0,
        float critMul = 1, int handCount = 6, int arena = 50, int damageThreshold = 0)
    {
        this.power = power;
        this.spirit = spirit;
        this.weight = weight;
        this.reflexes = reflex;
        this.critical = critical;
        this.damageAdd = damageAdd;
        this.damageMul = damageMul;
        this.critAdd = critAdd;
        this.critMul = critMul;
        this.handCount = handCount;
        this.arena = arena;
        this.damageThreshold = damageThreshold;
    }
}