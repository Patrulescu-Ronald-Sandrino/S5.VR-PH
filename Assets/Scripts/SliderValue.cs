using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public Text sliderValueText;
    public Slider slider;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderValueText.text = $"{(int)(slider.value * 100)} / 100";
    }
}
