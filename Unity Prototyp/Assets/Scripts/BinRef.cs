using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinRef : MonoBehaviour
{

    public GameObject BinLeft;
    public GameObject BinRight;

    public int closestNorternHub;

    private int RowLength = 8;
    public GameObject RowLeft;
    public GameObject RowRight;
    public GameObject Waypoint { get; set; }
    public GameObject WaypointList;
    public Vector2Int WaypointIndex { get; set; }

    public bool inOrderList = false;
    public bool notTracked = false;
    public bool notPicked = false;
    public bool wasPicked = false;
    public bool leftRow;

    private void Start()
    {
        //BinLeft = RowLeft.transform.GetChild(this.transform.GetSiblingIndex() -1 - closestNorternHub).gameObject;
        //BinRight = RowRight.transform.GetChild(this.transform.GetSiblingIndex() -1 - closestNorternHub).gameObject;
        //Wayp

        int index = gameObject.transform.GetSiblingIndex();
        int offset = 0;

        offset = index / RowLength + 1;
        int offsetWaypointIndex = index + offset;

        Waypoint = WaypointList.transform.GetChild(offsetWaypointIndex).gameObject;
        if (leftRow)
        {
            Waypoint.GetComponent<WaypointRef>().BinLeft = this.gameObject;
        }
        else 
        { 
            Waypoint.GetComponent<WaypointRef>().BinRight = this.gameObject;
        }
        WaypointIndex = new Vector2Int(Waypoint.transform.GetSiblingIndex(), WaypointList.transform.GetSiblingIndex());
        

    }
    //WHY HAVE I TRIED THIS SHIT
    /*
       int offset = 1;
       int index = gameObject.transform.GetSiblingIndex();
       int Multipier = index / 8;
       if (index != 1)
       {
           if (index % 7 == 1)
           {
               offset = (Multipier) - 1;

           }
           else
           {
               offset = (Multipier) ;
           }
       }
       else
       {
           offset = (Multipier);
       }
       */
}
