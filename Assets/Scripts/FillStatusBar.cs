using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image fillImage;
    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (slider.value <= slider.minValue) {
            fillImage.enabled = false;
        }

        if (slider.value > slider.minValue && !fillImage.enabled) {
            fillImage.enabled = true;
        }
        float fillValue = playerHealth.currentHealth / playerHealth.maxHealth;

        //made to change color of health based on how much left
        //do Color.<any color you want> to change it
        if (fillValue <= slider.maxValue / 3) { 
            fillImage.color = Color.red;
        } else if (fillValue > slider.maxValue / 3) {
            fillImage.color = Color.blue;
        }
        slider.value = fillValue;
    }
}
