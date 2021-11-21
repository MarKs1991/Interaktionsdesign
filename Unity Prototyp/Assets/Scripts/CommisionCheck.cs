using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommisionCheck : MonoBehaviour
{
    public RouteVisualizer routeVisualizer;

    public List<Transform> WaggonBoxPositions;
    private int placedBoxes = 0;
    public int PathIndex = 1;
   

    private void OnTriggerEnter(Collider other)
    {
        //if GameObject is a Good/Box place on Waggon
        if (other.gameObject.layer == 6)
        {

            WaggonBoxPositions[placedBoxes].GetComponent<MeshFilter>().mesh = other.transform.GetComponent<MeshFilter>().mesh;
            WaggonBoxPositions[placedBoxes].GetComponent<Renderer>().material = other.transform.GetComponent<Renderer>().material;
            WaggonBoxPositions[placedBoxes].GetComponent<MeshRenderer>().enabled = true;
            placedBoxes++;

            BinRef _bin = other.gameObject.transform.parent.GetComponent<BinRef>();
            _bin.addItemToCart();
            bool allItemsFromBinCollected = _bin.checkRequiredAmount();

            Destroy(other.gameObject);
            if (allItemsFromBinCollected)
            {
                routeVisualizer.RenderRoute(PathIndex, PathIndex + 1);
                PathIndex++;
            }
        
        }
    }
}
