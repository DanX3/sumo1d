using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class IncreaseAttributeRow : MonoBehaviour, IDeselectHandler
{

    public bool selected { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(name + " deselected");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Choose()
    {
        foreach (var row in FindObjectsOfType<IncreaseAttributeRow>())
            row.Select(false);
    }

    [SerializeField] 

    public void Select(bool select)
    {
        if (select)
            foreach (var row in FindObjectsOfType<IncreaseAttributeRow>())
                row.Select(false);

        // ui adjustment
        var button = GetComponent<Button>();
        var color = button.image.color;
        color.a = select ? 1f : 0.5f;
        button.image.color = color;

        attributeLabel.text = attributeValue + (select ? 1 : 0) + "";
    }

    public PlayerAttribute stat;
    [SerializeField] TMPro.TMP_Text attributeLabel;
    int attributeValue;

    public void SetAttribute(Player player)
    {
        switch (stat)
        {
            case PlayerAttribute.Power: attributeValue = player.stats.baseStats.power; break;
            case PlayerAttribute.Spirit: attributeValue = player.stats.baseStats.spirit; break;
            case PlayerAttribute.Weight: attributeValue = player.stats.baseStats.weight; break;
            case PlayerAttribute.Reflex: attributeValue = player.stats.baseStats.reflexes; break;
            case PlayerAttribute.Critical: attributeValue = player.stats.baseStats.critical; break;
        }

        Debug.Log(name + " set attributeValue " + attributeValue);

        attributeLabel.text = attributeValue + "";
    }


}
