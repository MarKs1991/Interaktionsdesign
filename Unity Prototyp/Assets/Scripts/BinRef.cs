using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinRef : MonoBehaviour
{

    private int RowLength = 8;
    public GameObject RowLeft;
    public GameObject RowRight;
    public GameObject Waypoint { get; set; }
    public GameObject WaypointList;
    public Vector2Int WaypointIndex { get; set; }
    public Vector2Int BinIndex { get; set; }
    public int ItemAmountinOrder { get; set; }

    public bool inOrderList = false;
    public bool notTracked = false;
    public bool notPicked = false;
    public bool wasPicked = false;
    public bool leftRow;

    private void Start()
    {       
        int RowIndex = gameObject.transform.GetSiblingIndex();
        int CollumnIndex = gameObject.transform.parent.GetSiblingIndex();
        BinIndex = new Vector2Int(CollumnIndex - 1, RowIndex - 1);


        int offset = 0;
        offset = RowIndex / RowLength + 1;
        int offsetWaypointIndex = RowIndex + offset;

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
