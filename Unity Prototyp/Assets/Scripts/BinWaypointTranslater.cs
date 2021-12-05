using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinWaypointTranslater : MonoBehaviour
{
    public WarehouseNavigation warehouseNavigation;
    public List<Vector3Int> OrderBins;
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

    public void TranslateBinToWaypoint()
    {
        int i = 0;
        foreach (var itemBin in OrderBins)
        {
            int CollumnIndex = itemBin.x;
            int RowIndex = itemBin.y - 1;
            Debug.Log(itemBin.x);
            GameObject Bin = Rows[CollumnIndex - 1].transform.GetChild(RowIndex).gameObject;

            Bin.GetComponent<BinRef>().ItemAmountinOrder = itemBin.z;
            Bin.GetComponent<BinRef>().LeftOverAmountinOrder = itemBin.z;

            Vector2Int WaypointIndex = Bin.GetComponent<BinRef>().WaypointIndex;
            Bin.GetComponent<BinRef>().inOrderList = true;
            Bin.GetComponent<BinRef>().notPicked = true;
            Bin.GetComponent<BinRef>().notTracked = true;
            SearchedWaypoints.Add(WaypointIndex);
            //if(i == OrderBins.Count - 1)
           // {
           //     Bin.GetComponent<BinRef>().lastItem = true;
            //}
            //i++;
        }


        warehouseNavigation.calculateRoutes(SearchedWaypoints, OrderBins);
    }
    private void clearList()
    {
        SearchedWaypoints.Clear();
    }

    /*
    private void sortOrder(List<Vector2Int> _Order, List<Vector2Int> Waypoints)
    {
        List<Vector2Int> order = _Order;
        List<Vector2Int> items = Waypoints;

        Dictionary<int, Item> d = items.ToDictionary(x => x.ID);

        List<Vector2Int> ordered = order.Select(i => d[i]).ToList();
    }
    */
}
