using DG.Tweening;
using UnityEngine;

public class ScalePlayer : CardModifier
{
    public float scaleAmount;

    public override void Apply(Player user)
    {
        // Debug.Log($"AlterAttribute of {gameObject.name} added");
        foreach (Player target in targets)
            target.transform.DOScale(scaleAmount, 1.5f)
                .SetEase(Ease.InOutCubic)
                .Play();
    }

    public override void Remove(Player user)
    {
        // Debug.Log($"AlterAttribute of {gameObject.name} removed");
        foreach (Player target in targets)
            target.transform.DOScale(1f / scaleAmount, 1.5f)
                .SetEase(Ease.InOutCubic)
                .Play();
    }
}