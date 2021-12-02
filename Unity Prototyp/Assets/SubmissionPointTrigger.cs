using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmissionPointTrigger : MonoBehaviour
{
    [SerializeField] private Transform CartSubmissionPoint;
    [SerializeField] private Transform Cart;
    [SerializeField] private TextMeshProUGUI Notification;

    private void OnTriggerEnter(Collider other)
    {
        //if Player Hits Collider
        if (other.gameObject.layer == 7 || other.gameObject.layer == 9)
        {
            
            Cart.transform.position = CartSubmissionPoint.transform.position;
            Cart.transform.rotation = CartSubmissionPoint.transform.rotation;
            Cart.transform.parent = CartSubmissionPoint.transform;
            Notification.gameObject.SetActive(true);
            Notification.text = "Bestellung abgeliefert";
            
            this.gameObject.SetActive(false);
        }
    }
}
