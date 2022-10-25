using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerup : MonoBehaviour
{
    [SerializeField] PowerupTooltip tooltipPrefab;
    [SerializeField] Transform parent;

    PowerupTooltip tooltip;
    public string powerupMessage;

    public float tooltipOffset { get => transform.position.x < 0.5f * Screen.width ? 200f : -50f ; }

void Start()
    {
        if (transform.position.x > 0.5f * Screen.width)
            parent.transform.localScale = new Vector3(-1f, 1f, 1f);
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
}
