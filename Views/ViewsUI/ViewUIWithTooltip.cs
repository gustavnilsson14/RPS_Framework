using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(EventTrigger))]
public class ViewUIWithTooltip : ViewUI
{
    [Header("Tooltip")]
    public Animator tooltipAnimator;
    public TextMeshProUGUI textField;
    public TextMeshProUGUI descriptionField;
    private EventTrigger eventTrigger;
    private DescribedDesign describedDesign;

    public override void Awake()
    {
        base.Awake();
        if (tooltipAnimator == null)
            return;
        tooltipAnimator.SetTrigger("Hide");
        eventTrigger = GetComponent<EventTrigger>();
    }

    public override void Start()
    {
        base.Start();
        AddTriggers();
        MoveToTop();
    }

    public override void Render(Design design)
    {
        base.Render(design);
        describedDesign = design as DescribedDesign;
        textField.text = describedDesign.GetName();
        descriptionField.text = describedDesign.GetDescription();
    }

    public void OnMouseOver()
    {
        if (tooltipAnimator == null)
            return;
        tooltipAnimator.SetTrigger("Show");
    }
    public void OnMouseOut()
    {
        if (tooltipAnimator == null)
            return;
        tooltipAnimator.SetTrigger("Hide");
    }

    private void MoveToTop()
    {
        if (tooltipAnimator == null)
            return;
        tooltipAnimator.transform.SetParent(GetComponentInParent<Canvas>().transform, true);
        tooltipAnimator.enabled = true;
    }

    private void AddTriggers()
    {
        if (tooltipAnimator == null)
            return;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnMouseOver(); });
        eventTrigger.triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { OnMouseOut(); });
        eventTrigger.triggers.Add(entry);
    }
}
