using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinRef : MonoBehaviour
{

    private int RowLength = 8;
    public GameObject Waypoint { get; set; }
    public GameObject WaypointList;
    public Vector2Int WaypointIndex { get; set; }
    
    public Vector2Int BinIndex;
    public string BinItem;
    public int LeftOverAmountinOrder = 0;
    public int ItemAmountinOrder = 0;

    public bool inOrderList = false;
    public bool notTracked = false;
    public bool notPicked = false;
    public bool wasPicked = false;

    private void Start()
    {       
        int RowIndex = gameObject.transform.GetSiblingIndex();
        int CollumnIndex = gameObject.transform.parent.GetSiblingIndex();
        BinIndex = new Vector2Int(CollumnIndex + 1, RowIndex + 1);


        int offset = 0;
        offset = RowIndex / RowLength + 1;
        int offsetWaypointIndex = RowIndex + offset;

        Waypoint = WaypointList.transform.GetChild(offsetWaypointIndex).gameObject;
        
        WaypointIndex = new Vector2Int(Waypoint.transform.GetSiblingIndex(), WaypointList.transform.GetSiblingIndex());

        //Platzhalter
        string[] weinArray = { "Chardonnay", "Gewürztraminer", "Müller-Thurgau", "Gewürztraminer", "Muskateller", "Riesling", "Sauvignon Blanc", "Cabernet Sauvignon"};
        BinItem = weinArray[Random.Range(0,7)];
    }
    public void addItemToCart()
    {
        LeftOverAmountinOrder = LeftOverAmountinOrder - 1;

    }
    public bool checkRequiredAmount()
    {
        if (LeftOverAmountinOrder == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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
