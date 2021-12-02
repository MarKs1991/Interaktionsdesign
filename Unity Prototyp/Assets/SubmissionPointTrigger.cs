using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmissionPointTrigger : MonoBehaviour
{
    [SerializeField] private Transform CartSubmissionPoint;
    [SerializeField] private Transform Cart;

    private void OnTriggerEnter(Collider other)
    {
        //if Player Hits Collider
        if (other.gameObject.layer == 7)
        {
            Cart = CartSubmissionPoint;
        }
    }
}
