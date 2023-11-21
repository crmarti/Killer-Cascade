using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxExp(int exp)
    {
        slider.maxValue = exp;
        slider.value = 0;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetExp(int exp)
    {
        slider.value = exp;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
