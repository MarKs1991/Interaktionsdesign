using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommisionCheck : MonoBehaviour
{
    public RouteVisualizer routeVisualizer;
    public OrderDisplay orderDisplay;

    public List<Transform> WaggonBoxPositions;
    private int placedBoxes = 0;
    public int PathIndex = 1;
   

    private void OnTriggerEnter(Collider other)
    {
        //if GameObject is a Good/Box place on Waggon
        if (other.gameObject.layer == 6)
        {
            BinRef _bin = other.gameObject.transform.parent.GetComponent<BinRef>();
            if (_bin.LeftOverAmountinOrder > 0)
            {

                WaggonBoxPositions[placedBoxes].GetComponent<MeshFilter>().mesh = other.transform.GetComponent<MeshFilter>().mesh;
                WaggonBoxPositions[placedBoxes].GetComponent<Renderer>().material = other.transform.GetComponent<Renderer>().material;
                WaggonBoxPositions[placedBoxes].GetComponent<MeshRenderer>().enabled = true;
                placedBoxes++;


                _bin.addItemToCart();
                bool rightCollected = orderDisplay.UpdateBins(_bin);


                bool allandRightItemsCollected = _bin.checkRequiredAmount();

                Destroy(other.gameObject);
                if (allandRightItemsCollected)
                {
                    orderDisplay.checkComplete(_bin);
                    routeVisualizer.RenderRoute(PathIndex, PathIndex + 1);
                    PathIndex++;
                }

            }
        }
        Debug.Log("no more needed");
    }
}
