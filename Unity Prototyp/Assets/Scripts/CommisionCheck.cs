using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommisionCheck : MonoBehaviour
{
    public RouteVisualizer routeVisualizer;
    public OrderDisplay orderDisplay;

    public List<Transform> WaggonBoxPositions;
    private int placedBoxes = 0;
    public int PathIndex = 1;
    public TextMeshProUGUI Notification;


    private void OnTriggerEnter(Collider other)
    {

        //if GameObject is a Good/Box place on Waggon
        if (other.gameObject.layer == 6)
        {
            if (other.gameObject.transform.GetComponent<Item>().binRef != null)
            {
                BinRef _bin = other.gameObject.transform.GetComponent<Item>().binRef;
                if (_bin.LeftOverAmountinOrder > 0)
                {

                    WaggonBoxPositions[placedBoxes].GetComponent<MeshFilter>().mesh = other.transform.GetComponent<MeshFilter>().mesh;
                    WaggonBoxPositions[placedBoxes].GetComponent<Renderer>().material = other.transform.GetComponent<Renderer>().material;
                    WaggonBoxPositions[placedBoxes].GetComponent<MeshRenderer>().enabled = true;



                    _bin.addItemToCart();
                    bool rightCollected = orderDisplay.UpdateBins(_bin);


                    bool allandRightItemsCollected = _bin.checkRequiredAmount();
                    placedBoxes++;
                    Destroy(other.gameObject);
                    if (allandRightItemsCollected)
                    {

                        orderDisplay.checkComplete(_bin);
                        //if (!_bin.lastItem)
                        {
                            routeVisualizer.RenderRoute(PathIndex, PathIndex + 1);
                            PathIndex++;
                            //}

                        }
                    }
                }
                else
                {
                    Notification.text = "Keine weiteren Güter von diesem Lagerplatz mehr nötig!";
                    Notification.color = new Color(0.8f, 0, 0, 0.75f);
                    Notification.GetComponent<Animation>().Play();
                    Notification.transform.parent.GetComponent<Animation>().Play();
                }

            }
            
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if(Notification.text.Equals(Notification.text = "Keine weiteren Güter von diesem Lagerplatz mehr nötig!"))
            Notification.text = "";
    }
    */
}
