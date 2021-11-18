using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteVisualizer : MonoBehaviour
{
    public WarehouseNavigation warehouseNavigation;
    public BinWaypointTranslater binWaypointTranslater;
    public GameObject[] BinRows;
    public GameObject Arrow;
    public GameObject ArrowList;
 
    //private Vector2Int [] waypoints;
    public LineRenderer RouteLine;

    public List<Vector2Int> SubWaypoints { get; set; }
    public List<Vector2Int> waypoints { get; set; }
    public List<bool> isBin { get; set; }
    public List<int> Breakpoints { get; set; }
    public int PathIndex { get; set; }

    private List<int> breaks;
    public void RenderRoute(int startIndex, int endIndex)
    {

        Debug.Log(string.Join(",", Breakpoints));
        startIndex = Breakpoints[startIndex];
        endIndex = Breakpoints[endIndex];

        List<Vector2Int> BinList = new List<Vector2Int>(binWaypointTranslater.OrderBins);


        destroyOldArrows();


        //List<Vector3> BinWaypoint1 = new List<Vector3>(waypoints, waypoints.y,);
        Vector3 NextWaypoint = new Vector3();
        RouteLine.positionCount = (endIndex - startIndex) + 1;
        for (int i = startIndex; i <= endIndex; i++)
        {

            if (isBin[i])
            {
                if (BinRows[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].y).GetComponent<BinRef>() != null)
                {
                    RouteLine.SetPosition(i - startIndex, BinRows[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).GetComponent<WaypointRef>().BinLeft.transform.position);
                    Debug.Log(BinRows[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).GetComponent<WaypointRef>().BinLeft.name.ToString());
                }
            }
            else
            {
                Vector3 BinWaypoint = BinRows[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).transform.position;

                if (i != SubWaypoints.Count - 1 && !isBin[i + 1])
                {
                    NextWaypoint = BinRows[SubWaypoints[i + 1].y].transform.GetChild(SubWaypoints[i + 1].x).transform.position;
                }
                else if (i != SubWaypoints.Count - 1 && isBin[i + 1])
                {
                    NextWaypoint = BinRows[SubWaypoints[i + 1].y].transform.GetChild(SubWaypoints[i + 1].x).GetComponent<WaypointRef>().BinLeft.transform.position;
                    //NextWaypoint = BinRows[BinList[i].y].GetComponent<BinRef>().gameObject.transform.position;
                }

                RouteLine.SetPosition(i - startIndex, BinWaypoint);

                GameObject arrowInst = GameObject.Instantiate(Arrow, new Vector3(BinWaypoint.x, 5, BinWaypoint.z), Quaternion.identity, ArrowList.transform);


                arrowInst.transform.LookAt(NextWaypoint);
                //arrowInst.transform.localEulerAngles = new Vector3(0, arrowInst.transform.localEulerAngles.y, 0);
                Debug.Log(NextWaypoint);
            }
        }
        Debug.Log(string.Join(",", Breakpoints));
    }

    private void destroyOldArrows()
    {
        for (int i = 0; i <= ArrowList.transform.childCount - 1; i++)
        {
            Destroy(ArrowList.transform.GetChild(i).gameObject);
        }
    }
}
