using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteVisualizer : MonoBehaviour
{
    public WarehouseNavigation warehouseNavigation;
    public BinWaypointTranslater binWaypointTranslater;
    public GameObject[] BinRows;
    public GameObject[] WaypointCollumns;
    public GameObject Arrow;
    public GameObject ArrowList;
    private bool wasPicked;

    //private Vector2Int [] waypoints;
    public LineRenderer RouteLine;

    public List<Vector2Int> SubWaypoints { get; set; }
    public List<Vector2Int> waypoints { get; set; }
    public List<bool> isBin { get; set; }
    public List<int> Breakpoints { get; set; }
    public int PathIndex { get; set; }

    public Vector3 LastWaypoint;

    private List<int> breaks;
    public void RenderRoute(int startIndex, int endIndex)
    {

        //Debug.Log(string.Join(",", Breakpoints));
        startIndex = Breakpoints[startIndex];
        endIndex = Breakpoints[endIndex];

        //List<Vector2Int> BinList = new List<Vector2Int>(binWaypointTranslater.OrderBins);


        destroyOldArrows();


        //List<Vector3> BinWaypoint1 = new List<Vector3>(waypoints, waypoints.y,);
        Vector3 NextWaypoint = new Vector3();



        RouteLine.positionCount = (endIndex - startIndex) + 1;


        for (int i = startIndex; i <= endIndex; i++)
        {
            if (WaypointCollumns[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).GetComponent<WaypointRef>() != null)
            {
                WaypointRef waypointRef = WaypointCollumns[SubWaypoints[i].y].gameObject.transform.GetChild(SubWaypoints[i].x).gameObject.GetComponent<WaypointRef>();

                //RouteLine.SetPosition(i - startIndex, WaypointCollumns[SubWaypoints[i].y].gameObject.transform.GetChild(SubWaypoints[i].x).gameObject.GetComponent<WaypointRef>().BinLeft.transform.position);
                if (waypointRef.BinLeft != null && (waypointRef.BinRight != null))
                {
                    if (waypointRef.BinLeft.GetComponent<BinRef>().wasPicked)
                    {
                        RouteLine.SetPosition(i - startIndex, waypointRef.BinLeft.transform.position);
                        //LastWaypoint = waypointRef.BinLeft.transform.position;
                        waypointRef.BinLeft.GetComponent<BinRef>().notPicked = false;
                        waypointRef.BinLeft.GetComponent<BinRef>().wasPicked = true;

                    }


                    else if (waypointRef.BinRight.GetComponent<BinRef>().wasPicked)
                    {
                        RouteLine.SetPosition(i - startIndex, waypointRef.BinRight.transform.position);
                        //LastWaypoint = waypointRef.BinRight.transform.position;
                        waypointRef.BinRight.GetComponent<BinRef>().notPicked = false;
                        waypointRef.BinRight.GetComponent<BinRef>().wasPicked = true;
                    }
                }
                //Debug.Log(BinRows[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).GetComponent<WaypointRef>().BinLeft.name.ToString());
            }



            if (isBin[i])
            {
                if (WaypointCollumns[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).GetComponent<WaypointRef>() != null)
                {
                    WaypointRef waypointRef = WaypointCollumns[SubWaypoints[i].y].gameObject.transform.GetChild(SubWaypoints[i].x).gameObject.GetComponent<WaypointRef>();

                    //RouteLine.SetPosition(i - startIndex, WaypointCollumns[SubWaypoints[i].y].gameObject.transform.GetChild(SubWaypoints[i].x).gameObject.GetComponent<WaypointRef>().BinLeft.transform.position);
                    if (waypointRef.BinLeft != null && (waypointRef.BinRight != null))
                    {
                        if (waypointRef.BinLeft.GetComponent<BinRef>().inOrderList && waypointRef.BinLeft.GetComponent<BinRef>().notPicked)
                        {
                            RouteLine.SetPosition(i - startIndex, waypointRef.BinLeft.transform.position);
                            //LastWaypoint = waypointRef.BinLeft.transform.position;
                            waypointRef.BinLeft.GetComponent<BinRef>().notPicked = false;
                            waypointRef.BinLeft.GetComponent<BinRef>().wasPicked = true;
                        }

                        else if (waypointRef.BinRight.GetComponent<BinRef>().inOrderList && waypointRef.BinRight.GetComponent<BinRef>().notPicked)
                        {
                            RouteLine.SetPosition(i - startIndex, waypointRef.BinRight.transform.position);
                            //LastWaypoint = waypointRef.BinRight.transform.position;
                            waypointRef.BinRight.GetComponent<BinRef>().notPicked = false;
                            waypointRef.BinRight.GetComponent<BinRef>().wasPicked = true;
                        }
                    }

                    //Debug.Log(BinRows[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).GetComponent<WaypointRef>().BinLeft.name.ToString());
                }
            }
            else
            {
                Vector3 BinWaypoint = WaypointCollumns[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).transform.position;

                if (i != SubWaypoints.Count - 1 && !isBin[i + 1])
                {
                    NextWaypoint = WaypointCollumns[SubWaypoints[i + 1].y].transform.GetChild(SubWaypoints[i + 1].x).transform.position;
                }
                else if (i != SubWaypoints.Count - 1 && isBin[i + 1])
                {
                    //NextWaypoint = BinRows[SubWaypoints[i + 1].y].transform.GetChild(SubWaypoints[i + 1].x).GetComponent<BinRef>().BinLeft.transform.position;
                    //Debug.Log(SubWaypoints[i]);
                    WaypointRef waypointRef = WaypointCollumns[SubWaypoints[i + 1].y].gameObject.transform.GetChild(SubWaypoints[i + 1].x).gameObject.GetComponent<WaypointRef>();

                    if (waypointRef.BinLeft.GetComponent<BinRef>().inOrderList && waypointRef.BinLeft.GetComponent<BinRef>().notTracked)
                    {
                        NextWaypoint = waypointRef.BinLeft.transform.position;
                        waypointRef.BinLeft.GetComponent<BinRef>().notTracked = false;                       
                    }
                    else if (waypointRef.BinRight.GetComponent<BinRef>().inOrderList && waypointRef.BinLeft.GetComponent<BinRef>().notTracked)
                    {
                        NextWaypoint = waypointRef.BinRight.transform.position;
                        waypointRef.BinRight.GetComponent<BinRef>().notTracked = false;                       
                    }
                }

                RouteLine.SetPosition(i - startIndex, BinWaypoint);

                GameObject arrowInst = GameObject.Instantiate(Arrow, new Vector3(BinWaypoint.x, 5, BinWaypoint.z), Quaternion.identity, ArrowList.transform);
                arrowInst.transform.LookAt(NextWaypoint);
                arrowInst.transform.localEulerAngles = new Vector3(0, arrowInst.transform.localEulerAngles.y, 0);
            }        
        }
        //Debug.Log(string.Join(",", Breakpoints));
    }


    private void destroyOldArrows()
    {
        for (int i = 0; i <= ArrowList.transform.childCount - 1; i++)
        {
            Destroy(ArrowList.transform.GetChild(i).gameObject);
        }
    }
}
