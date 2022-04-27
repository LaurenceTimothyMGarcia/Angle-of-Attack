using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Slider instructions:
1) Add UI/Slider into the hierarchy
2) Remove the Handle Slide Area
3) Go to Slider - uncheck Interactable, Transition set to none, Value slide put to 1 or just move knob from left to right, under Rect Transform - click the square to move the health bar to a certain location of your choosing and use coords x and y to adjust location, personal setting is top left with pos x = 150 and pos y = -50
4) Go to Fill - change color to red or whatever color you want, the slider thing will look incomplete so move the fill bar length to the extent of the background length
5) Attach FillStatusBar.cs to Slider and attach Fill to Fill Image
6) Attach PlayerHealth.cs to the desired player
7) On PlayerHealth.cs, the health value can be set to whatever and current health will adjust to max health at the start always, animation code is set for hit animation, please look at youtuber BMo for the health bar tutorial if other stuff doesnt make sense (namely the animation stuff) :)
*/
public class FillStatusBar : MonoBehaviour
{
    public PlayerHealth playerHealth; //PlayerHealth.cs file must be put on the player 
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
