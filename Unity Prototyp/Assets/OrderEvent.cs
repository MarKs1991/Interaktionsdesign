using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderEvent : MonoBehaviour
{
    public BinWaypointTranslater binWaypointTranslater;
    private bool isScanned = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if Scanner 
        if(other.gameObject.layer == 8)
        scanOrder();
    }

    void scanOrder()
    {
        if (!isScanned)
        {
            binWaypointTranslater.TranslateBinToWaypoint();
            
        }
    }
}
