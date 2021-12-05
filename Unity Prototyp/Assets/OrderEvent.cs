using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderEvent : MonoBehaviour
{
    public BinWaypointTranslater binWaypointTranslater;
    public HololensTrigger hololensTrigger;
    private bool isScanned = false;

    // Update is called once per frame
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if Scanner 
        if(other.gameObject.layer == 10)
        {
            if(hololensTrigger.hololensOn)
            {
                scanOrder();
            }          
        }       
    }

    void scanOrder()
    {
        if (!isScanned)
        {
            binWaypointTranslater.TranslateBinToWaypoint();           
        }
    }
}
