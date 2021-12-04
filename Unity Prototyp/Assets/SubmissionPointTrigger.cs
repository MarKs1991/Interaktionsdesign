using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmissionPointTrigger : MonoBehaviour
{
    [SerializeField] private Transform CartSubmissionPoint;
    [SerializeField] private Transform Cart;
    [SerializeField] private TextMeshProUGUI Notification;
    [SerializeField] private OrderDisplay orderDisplay;

    private void OnTriggerEnter(Collider other)
    {
        //if Player Hits Collider
        if (other.gameObject.layer == 7 || other.gameObject.layer == 9)
        {
            
            Cart.transform.position = CartSubmissionPoint.transform.position;
            Cart.transform.rotation = CartSubmissionPoint.transform.rotation;
            Cart.transform.parent = CartSubmissionPoint.transform;
            Notification.text = "Bestellung abgeliefert";
            Notification.color = new Color(0, 0.8f, 0, 0.75f);
            Notification.gameObject.GetComponent<Animation>().Play();
            orderDisplay.checkCompletion();
            this.gameObject.SetActive(false);
        }
    }
}
