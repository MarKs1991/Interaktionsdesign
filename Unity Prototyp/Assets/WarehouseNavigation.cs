using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarehouseNavigation : MonoBehaviour
{
    public List<int> Order;
    public int[][] WaypointArray;
    public List<int> Hubs;
    public int CollumnLength;

    public List<int> targetHubs;
    public List<int> EmployeeHubs;

    public Vector2Int employeePosition;
    //public int employeePositionRow;

    public Vector2Int[] targetPos;
    //public List<int> targetPosRows;

    public int traveledDistance = 0;
    public List<int> DistanceList;

    int shortestRoute = 1000000;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            calculateRoutes();
        }
    }

    private void calculateRoutes()
    {
        int Routes = 1;
        int shortestRoute = 1000000;
        var vals = targetPos;
        foreach (var v in Permutations(vals))
        {
            //DistanceList.Clear();
            traveledDistance = 0;
            for (int i = 0; i <= targetPos.Length - 1; i++)
            {
                findClosedCollumnToTarget(targetPos[i].x, targetHubs);
                int nearestHub = findClosedCollumnToEmployee(employeePosition.x);

                travelToHub(nearestHub);

                if (traveledDistance >= shortestRoute)
                {
                    break;
                }

                changeRow(targetPos[i].y);
                travelToTarget(targetPos[i].x);
                resetLists();

                if(traveledDistance >= shortestRoute)
                {
                    break;
                }
            }            

            if(traveledDistance < shortestRoute)
            {
                shortestRoute = traveledDistance;
                Debug.Log(string.Join(",", v) + "is the shortest Route with " + shortestRoute + "Steps");                
            }
            
        }
    }
   

    private void findClosedCollumnToTarget(int target, List<int> ClosestHubs)
    {
        foreach (int Hub in Hubs)
        {
            if (Mathf.Abs(target - Hub) <= CollumnLength)
            {
                ClosestHubs.Add(Hub);
            }
        }
    }
    private int findClosedCollumnToEmployee(int employeePos)
    {
        /*
        foreach (int Hub in Hubs) 
        {
            if(employeePos == Hub)
            {
                break;
            }
        }
        */
        int distance1 = Mathf.Abs(targetHubs[0] - employeePosition.x);
        int distance2 = Mathf.Abs(targetHubs[1] - employeePosition.x);

        int nearestHub = distance1 < distance2 ? targetHubs[0] : targetHubs[1];
        //Debug.Log(nearestHub);
        return nearestHub;
    }
    private void travelToHub(int targetedHub)
    {
        findClosedCollumnToTarget(employeePosition.y, EmployeeHubs);
        if (targetHubs[0] != EmployeeHubs[0] && targetHubs[1] != EmployeeHubs[1])
        {
            traveledDistance = traveledDistance + Mathf.Abs(targetedHub - employeePosition.x);
            //Debug.Log("backtohub");
            employeePosition.x = targetedHub;
        }
    }
    private void changeRow(int targetPosRow)
    {
        if (employeePosition.y != targetPosRow)
        {
            traveledDistance = traveledDistance + Mathf.Abs(targetPosRow - employeePosition.y);
            employeePosition.y = targetPosRow;
        }
    }
    private void travelToTarget(int targetPosCollumn)
    {
        traveledDistance = traveledDistance + Mathf.Abs(targetPosCollumn - employeePosition.x);
        employeePosition.x = targetPosCollumn;
    }
    private void resetLists()
    {
        targetHubs.Clear();
        EmployeeHubs.Clear();
        //DistanceList.Add(traveledDistance);
    }
    private void breakOut()
    {
        resetLists();
        
    }


   


public static IEnumerable<T[]> Permutations<T>(T[] values, int fromInd = 0)
    {
        if (fromInd + 1 == values.Length)
            yield return values;
        else
        {
            foreach (var v in Permutations(values, fromInd + 1))
                yield return v;

            for (var i = fromInd + 1; i < values.Length; i++)
            {
                SwapValues(values, fromInd, i);
                foreach (var v in Permutations(values, fromInd + 1))
                    yield return v;
                SwapValues(values, fromInd, i);
            }
        }
    }

    private static void SwapValues<T>(T[] values, int pos1, int pos2)
    {
        if (pos1 != pos2)
        {
            T tmp = values[pos1];
            values[pos1] = values[pos2];
            values[pos2] = tmp;
        }
    }

















}




