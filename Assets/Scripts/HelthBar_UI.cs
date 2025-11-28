using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar_UI : MonoBehaviour
{
    public Health health;
    public Slider slider;

    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
    }

    
    void Update()
    {
        slider.value = health.GetHealthPercent();
    }
}
