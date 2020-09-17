using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddOnViewSlider : AddOnView
{
    public AddOnEvent onSliderValueChange = new AddOnEvent();
    protected Slider slider;

    public override void Awake()
    {
        base.Awake();
        slider = GetComponentInChildren<Slider>();
        if (slider == null)
        {
            Destroy(this);
            return;
        }
        slider.onValueChanged.AddListener(OnSliderValueChange);
    }

    public void OnSliderValueChange(float newValue) {
        onSliderValueChange.Invoke(this);
    }

    public float GetSliderValue()
    {
        return slider.value;
    }

    public void SetMaxValue(int amount)
    {
        slider.maxValue = amount;
    }

    public void SetValue(int amount)
    {
        slider.value = amount;
    }
}
