using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class healthbar : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI healthValue;

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        healthValue.text = ""+ healthSlider.value;
    }
}
