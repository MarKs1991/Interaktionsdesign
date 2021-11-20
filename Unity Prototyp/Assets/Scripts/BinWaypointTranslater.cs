using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinWaypointTranslater : MonoBehaviour
{
    public WarehouseNavigation warehouseNavigation;
    public List<Vector2Int> OrderBins;
    public List<GameObject> Rows;
    List<Vector2Int> SearchedWaypoints = new List<Vector2Int>();
    List<GameObject> WaypointCollections;
    List<GameObject> Waypoints;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TranslateBinToWaypoint();
        }
    }

    private void TranslateBinToWaypoint()
    {
       
        foreach (var itemBin in OrderBins)
        {
            int CollumnIndex = itemBin.x - 1;
            int RowIndex = itemBin.y - 1;

            GameObject Bin = Rows[CollumnIndex].transform.GetChild(RowIndex).gameObject;


            Vector2Int WaypointIndex = Bin.GetComponent<BinRef>().WaypointIndex;
            Bin.GetComponent<BinRef>().inOrderList = true;
            Bin.GetComponent<BinRef>().notPicked = true;
            Bin.GetComponent<BinRef>().notTracked = true;
            SearchedWaypoints.Add(WaypointIndex);
          
        }
        Vector2Int[] SearchedWaypointsArray = SearchedWaypoints.ToArray();

        Vector2Int[] OrderBinArray = OrderBins.ToArray();

        warehouseNavigation.calculateRoutes(SearchedWaypointsArray, OrderBinArray);
    }
    private void clearList()
    {
        SearchedWaypoints.Clear();
    }
    
}
