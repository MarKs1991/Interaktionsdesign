using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteVisualizer : MonoBehaviour
{
    public WarehouseNavigation warehouseNavigation;
    public GameObject[] BinRows;
    //private Vector2Int [] waypoints;
    public LineRenderer RouteLine;
    void Start()
    { 
        //waypoints = GetComponent<WarehouseNavigation>().shortestCombination;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void renderLines(Vector2Int[] waypoints, List<Vector2Int> SubWaypoints)
    {
        
        RouteLine.positionCount = SubWaypoints.Count;
        for(int i = 0; i <= SubWaypoints.Count - 1; i++)
        {
            
                //Vector3[] waypoints3D = new Vector3[](waypoints.x, waypoints.y, 0);
                Vector3 BinWaypoint = BinRows[SubWaypoints[i].y-1].transform.GetChild(SubWaypoints[i].x-1).transform.position;
                RouteLine.SetPosition(i, BinWaypoint);
            
        }
    }
}
