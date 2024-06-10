using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnOffButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;

    public bool isOn = false;

    public string onText;
    public Color onColor;
    public string offText;
    public Color offColor;

    private void Start()
    {
        text.text = offText;
        image.color = offColor;
    }

    public void OnOff()
    {
        isOn = !isOn;

        CommunicationManager.Instance.isServerHost = isOn;

        if (isOn)
        {
            text.text = onText;
            image.color = onColor;
        }
        else
        {
            text.text = offText; 
            image.color = offColor;
        }
    }
}
