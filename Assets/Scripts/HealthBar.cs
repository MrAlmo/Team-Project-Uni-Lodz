using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public Slider slider;
    public Vector3 offset;

    void Update()
    {
        slider.value = health.GetHealthPercent();
    }

    private void LateUpdate()
    {
        if (transform.parent != null) {
            transform.position = transform.parent.position + offset;
        }

    }
}
