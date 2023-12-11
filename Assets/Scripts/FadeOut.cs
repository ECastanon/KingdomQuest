using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeOut : MonoBehaviour
{
    private Image myColor;
    private TextMeshProUGUI textColor;
    private float opacity = 0;
    public void OnReveal(string passedString)
    {
        opacity = 2;
        myColor = GetComponent<Image>();
        textColor = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        ChangeText(passedString);
    }
    void Update()
    {
        if(opacity > 0)
        {
            opacity -= Time.deltaTime;
            Color alpha = new Color(myColor.color.r, myColor.color.g, myColor.color.b, opacity);
            myColor.color = alpha;
            textColor.color = new Color(1,1,1,opacity);
        }
        if(opacity <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void ChangeText(string newString)
    {
        textColor.text = newString;
    }
}
