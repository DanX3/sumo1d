using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerup : MonoBehaviour
{
    [SerializeField] PowerupTooltip tooltipPrefab;
    [SerializeField] Transform parent;
    [SerializeField] TMPro.TMP_Text turnsLeftLabel;

    int turnsLeft;
    PowerupTooltip tooltip;
    public string powerupMessage;
    CardPowerup card;

    public float tooltipOffset { get => transform.position.x < 0.5f * Screen.width ? 200f : -50f; }

    void Start()
    {
        if (transform.position.x > 0.5f * Screen.width)
            parent.transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void Init(CardPowerup card)
    {
        powerupMessage = card.description;
        turnsLeftLabel.text = card.durationInTurns + "";
        turnsLeft = card.durationInTurns;
        this.card = card;
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
        tooltip = Instantiate(tooltipPrefab, transform.parent);
        tooltip.Init(powerupMessage, tooltipOffset);
        tooltip.transform.position = Input.mousePosition;
    }

    public void OnPointerExit()
    {
        if (tooltip == null)
            return;

        Destroy(tooltip.gameObject);
    }

    public void DescreaseTurnsLeft()
    {
        turnsLeftLabel.text = --turnsLeft + "";
        if (turnsLeft <= 0)
            Destroy(gameObject);
    }
}
