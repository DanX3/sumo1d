using System.Collections;
using UnityEngine;

public class UIPowerup : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] TMPro.TMP_Text turnsLeftLabel;
    [SerializeField] GameObject tooltip;
    [SerializeField] TMPro.TMP_Text tooltipText;

    int turnsLeft;
    CardPowerup card;
    float tooltipDurationInSeconds = 0.5f;
    IEnumerator hideTooltipCoroutine;

    public void Init(CardPowerup card)
    {
        this.card = card;
        tooltipText.text = card.description;
        UpdateTurnsLeft();

        card.OnRemoved += DestroyUIPowerup;
    }

    void OnDestroy()
    {
        card.OnRemoved -= DestroyUIPowerup;
    }

    public void DestroyUIPowerup()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter()
    {
        if (hideTooltipCoroutine != null)
            StopCoroutine(hideTooltipCoroutine);
        tooltip.gameObject.SetActive(true);
    }

    public void OnPointerExit()
    {
        hideTooltipCoroutine = HideTooltip();
        StartCoroutine(hideTooltipCoroutine);
    }

    private IEnumerator HideTooltip()
    {
        yield return new WaitForSeconds(tooltipDurationInSeconds);
        tooltip.gameObject.SetActive(false);
    }

    private void UpdateTurnsLeft()
    {
        turnsLeft = card.durationInTurns;
        turnsLeftLabel.text = turnsLeft.ToString();
        if (turnsLeft <= 0)
            Destroy(gameObject);
    }

    public void DescreaseTurnsLeft()
    {
        turnsLeft--;
        UpdateTurnsLeft();
    }
}
