using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteVisualizer : MonoBehaviour
{
    public WarehouseNavigation warehouseNavigation;
    public GameObject[] BinRows;
    public GameObject Arrow;
    public GameObject ArrowList;
 
    //private Vector2Int [] waypoints;
    public LineRenderer RouteLine;

    public List<Vector2Int> SubWaypoints { get; set; }
    public List<Vector2Int> waypoints { get; set; }
    public List<bool> isBin { get; set; }

    public void renderLines()
    {

        for(int i = 0; i <= ArrowList.transform.childCount - 1; i++)
        {
            Destroy(ArrowList.transform.GetChild(i).gameObject);
        }

        
        //List<Vector3> BinWaypoint1 = new List<Vector3>(waypoints, waypoints.y,);
        Vector3 NextWaypoint = new Vector3();
        RouteLine.positionCount = SubWaypoints.Count;
        for (int i = 0; i <= SubWaypoints.Count - 1; i++)
        {
            if (isBin[i])
            {
                if (BinRows[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].y).GetComponent<BinRef>() != null)
                {
                    RouteLine.SetPosition(i, BinRows[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).GetComponent<BinRef>().BinLeft.transform.position);
                    Debug.Log(BinRows[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).GetComponent<BinRef>().BinLeft.name.ToString());
                }
            }
            else 
            { 
                Vector3 BinWaypoint = BinRows[SubWaypoints[i].y].transform.GetChild(SubWaypoints[i].x).transform.position;

                if (i != SubWaypoints.Count - 1 && !isBin[i+1])
                {
                    NextWaypoint = BinRows[SubWaypoints[i + 1].y].transform.GetChild(SubWaypoints[i + 1].x).transform.position;
                }
                else if (i != SubWaypoints.Count - 1 && isBin[i+1])
                {
                    NextWaypoint = BinRows[SubWaypoints[i + 1].y].transform.GetChild(SubWaypoints[i + 1].x).GetComponent<BinRef>().BinLeft.transform.position;
                }

                RouteLine.SetPosition(i, BinWaypoint);
              
                GameObject arrowInst = GameObject.Instantiate(Arrow, new Vector3(BinWaypoint.x, 5, BinWaypoint.z), Quaternion.identity, ArrowList.transform);


                arrowInst.transform.LookAt(NextWaypoint);
                //arrowInst.transform.localEulerAngles = new Vector3(0, arrowInst.transform.localEulerAngles.y, 0);
                Debug.Log(NextWaypoint);
            }
            

        }
    }
    
}
