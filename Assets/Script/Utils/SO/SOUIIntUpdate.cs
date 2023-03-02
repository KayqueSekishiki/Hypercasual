using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiTextValue;

    void Start()
    {
        uiTextValue.text = "x 0000" + soInt.value;
    }

    void Update()
    {

        if (soInt.value < 0)
        {
            uiTextValue.text = "X " + soInt.value + " How did you do it?";
        }
        else if (soInt.value >= 0 && soInt.value < 10)
        {
            uiTextValue.text = "x 0000" + soInt.value;
        }
        else if (soInt.value >= 10 && soInt.value < 100)
        {
            uiTextValue.text = "x 000" + soInt.value;
        }
        else if (soInt.value >= 100 && soInt.value < 1000)
        {
            uiTextValue.text = "x 00" + soInt.value;
        }
        else if (soInt.value >= 1000 && soInt.value < 10000)
        {
            uiTextValue.text = "x 0" + soInt.value;
        }
        else if (soInt.value >= 10000 && soInt.value < 100000)
        {
            uiTextValue.text = "x 0" + soInt.value;
        }
        else
        {
            uiTextValue.text = "I'm Rich!";
        }
    }
}
