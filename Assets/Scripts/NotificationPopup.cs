using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationPopup : MonoBehaviour
{
    private Animator anim;
    private TextMeshProUGUI notifText;
    // Start is called before the first frame update
    void Start()
    {
        //Gets Animator and text child
        anim = GetComponent<Animator>();
        notifText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void SlideInNotif(string newText) //Call this when a notification is needed
    {
        notifText.text = newText; //Sets the notification to whatever is passed
        anim.Play("Notif_SlideIn");
        StartCoroutine(SlideOutNotif());
    }
    IEnumerator SlideOutNotif() //Removes the notification after 2 seconds. Called after SlideInNotif
    {
        yield return new WaitForSeconds(3);
        anim.Play("Notif_SlideOut");
    }
}