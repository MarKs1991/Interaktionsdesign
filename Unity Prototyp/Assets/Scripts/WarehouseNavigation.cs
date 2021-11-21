using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarehouseNavigation : MonoBehaviour
{


    public RouteVisualizer routeVisualizer;
    public List<int> Hubs;
    public int RowLength;

    public List<int> targetHubs;
    public List<int> EmployeeHubs;
    public List<Vector2Int> subWaypointsList = new List<Vector2Int>();
    //List<Vector2Int> shortestSubWaypointsList = new List<Vector2Int>();
    public List<bool> isBin = new List<bool>();
    public List<int> Breakpoints = new List<int>();

    public Vector2Int employeePosition;
    public Vector2Int initialEmployeePos;

    public int traveledDistance = 0;
    public Vector2Int[] _POS;


    int shortestRoute = 1000000;
    //public Vector2Int[] shortestCombination;

    private void Start()
    {
        initialEmployeePos = employeePosition;

    }

    // Update is called once per frame


    public void calculateRoutes(List<Vector2Int> targetPos)
    {
        //Vector2Int[] shortestCombination = new[] { new Vector2Int(0, 0), new Vector2Int(1, 1) };
        List<Vector2Int> shortestCombination = new List<Vector2Int>();
        List<Vector2Int> OrderBinList = new List<Vector2Int>();
        List<int> _BreakPoints = new List<int>();

        //_POS = targetPos;


        int shortestRoute = 1000000;
        List <Vector2Int> vals = targetPos;
        foreach (List<Vector2Int> v in Permutations(vals))
        {
         

            resetNavigation();

            saveSubWaypoints(employeePosition.x,employeePosition.y, false);
            Breakpoints.Add(subWaypointsList.Count-1);
            for (int i = 0; i <= targetPos.Count - 1; i++)
            {
                resetLists();
                if (employeePosition != targetPos[i])
                {
                    if (employeePosition.y != targetPos[i].y)
                    {
                        findClosedHubToTarget(targetPos[i].x, targetHubs);
                        int nearestHub = findClosedHubToEmployee(employeePosition.x);

                        travelToHub(nearestHub);

                        if (traveledDistance >= shortestRoute) break;

                        changeCollumn(targetPos[i].y);
                    }

                    if (traveledDistance >= shortestRoute) break;

                    travelToTarget(targetPos[i].x);
                    travelToBin();
                    backInLine();
                }
                else
                {
                    travelToBin();
                    backInLine();
                }
            }            

            if(traveledDistance < shortestRoute)
            {

                shortestRoute = traveledDistance;
              
                shortestCombination.Clear();
                //subWaypointsList = shortestSubWaypointsList; 

                //Debug.Log(string.Join(",", Breakpoints));

                //_BreakPoints = Breakpoints;

                //OrderBinList = OrderBins.ToList<Vector2Int>();

                shortestCombination = v.ToList<Vector2Int>();
                //Debug.Log(string.Join(",", shortestCombination) + "is the shortest Route with " + shortestRoute + "Steps");
                routeVisualizer.SubWaypoints = new List<Vector2Int>(subWaypointsList);
                routeVisualizer.waypoints = new List<Vector2Int>(shortestCombination);

                routeVisualizer.isBin = new List<bool>(isBin);

                routeVisualizer.Breakpoints = new List<int>(Breakpoints);

                routeVisualizer.RenderRoute(0,1);
                Debug.Log(string.Join(",", Breakpoints));
            }
            //Debug.Log(string.Join(",", _BreakPoints));
        }
        //Vector2Int[] shortestCombination1 = shortestCombination;
        Debug.Log(string.Join(",", shortestCombination) + "is the shortest Route with " + shortestRoute + "Steps");
        //Debug.Log(string.Join(",", OrderBinList) + "is the shortest Route with " + shortestRoute + "Steps");


    }

    private void findClosedHubToTarget(int target, List<int> ClosestHubs)
    {
        foreach (var Hub in Hubs)
        {
            /*
            if(Mathf.Abs(target - Hub) == 0)
            {
                throw new Exception("Kommisionierpositionen dürfen nich auf:"+ string.Join(",", Hub.ToString()) + " liegen dies sind Hubs");
            }
            */
            if (Mathf.Abs(target - Hub) <= RowLength)
            {
                ClosestHubs.Add(Hub);
            }
        }
    }

    private int findClosedHubToEmployee(int employeePos)
    {
        
        int distance1 = Mathf.Abs(targetHubs[0] - employeePosition.x);
        int distance2 = Mathf.Abs(targetHubs[1] - employeePosition.x);

        int nearestHub = distance1 < distance2 ? targetHubs[0] : targetHubs[1];
        //Debug.Log(nearestHub);
        return nearestHub;
    }

    private void travelToHub(int targetedHub)
    {
        findClosedHubToTarget(employeePosition.y, EmployeeHubs);
        //if (targetHubs[0] != EmployeeHubs[0] && targetHubs[1] != EmployeeHubs[1])
        //{
            traveledDistance = traveledDistance + Mathf.Abs(targetedHub - employeePosition.x);
            //Debug.Log("backtohub");
            employeePosition.x = targetedHub;
            saveSubWaypoints(employeePosition.x, employeePosition.y, false);
        //}
    }

    private void changeCollumn(int targetPosCollumn)
    {
        if (employeePosition.y != targetPosCollumn)
        {
            traveledDistance = traveledDistance + (Mathf.Abs(targetPosCollumn - employeePosition.y)*2);
            employeePosition.y = targetPosCollumn;
            saveSubWaypoints(employeePosition.x, employeePosition.y, false);
        }
    }

    private void travelToTarget(int targetPosRow)
    {
        traveledDistance = traveledDistance + Mathf.Abs(targetPosRow - employeePosition.x);
        employeePosition.x = targetPosRow;
        saveSubWaypoints(employeePosition.x, employeePosition.y, false);
    }
    private void travelToBin()
    {
        
        saveSubWaypoints(employeePosition.x, employeePosition.y, true);
        Breakpoints.Add(subWaypointsList.Count-1);
    }
    private void backInLine()
    {
        saveSubWaypoints(employeePosition.x, employeePosition.y, false);
    }

    private void resetLists()
    {
        targetHubs.Clear();
        EmployeeHubs.Clear();        
        //DistanceList.Add(traveledDistance);
    }

    private void resetNavigation()
    {
        traveledDistance = 0;
        employeePosition = initialEmployeePos;
        subWaypointsList.Clear();
        isBin.Clear();
        Breakpoints.Clear();
    }
    private void saveSubWaypoints(int x, int y, bool _isBin)
    {
        subWaypointsList.Add(new Vector2Int(x, y));
        isBin.Add(_isBin);
    }


    public static IEnumerable<List<Vector2Int>> Permutations<Vector2Int>(List<Vector2Int> values, int fromInd = 0)
    {
        if (fromInd + 1 == values.Count)
            yield return values;
        else
        {
            foreach (var v in Permutations(values, fromInd + 1))
                yield return v;

            for (var i = fromInd + 1; i < values.Count; i++)
            {
                SwapValues(values, fromInd, i);
                foreach (var v in Permutations(values, fromInd + 1))
                    yield return v;
                SwapValues(values, fromInd, i);
            }
        }
    }

    private static void SwapValues<Vector2Int>(List<Vector2Int> values, int pos1, int pos2)
    {
        if (pos1 != pos2)
        {
            Vector2Int tmp = values[pos1];
            values[pos1] = values[pos2];
            values[pos2] = tmp;
        }
    }
}




