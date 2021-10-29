using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseNavigation : MonoBehaviour
{
    public List<int> Order;
    public int[][] WaypointArray;
    public List<int> Hubs;
    public int CollumnLength;
    public int targetPosCollumn;
    public int targetPosRow;
    public List<int> targetHubs;
    public List<int> EmployeeHubs;
    public int employeePositionCollumn;
    public int employeePositionRow;
    public int traveledDistance = 0;


    // Start is called before the first frame update
    void Start()
    {
        findClosedCollumnToTarget(targetPosCollumn, targetHubs);
        int nearestHub = findClosedCollumnToEmployee(employeePositionCollumn);
        traveledDistance = 0;
        travelToHub(nearestHub);
        changeRow();
        travelToTarget();
        String str = "ABCDEF";
        int n = str.Length;
        permute(str, 0, n - 1);
    }
    // Update is called once per frame
    void Update()
    {

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

        int distance1 = Mathf.Abs(targetHubs[0] - employeePositionCollumn);
        int distance2 = Mathf.Abs(targetHubs[1] - employeePositionCollumn);

        int nearestHub = distance1 < distance2 ? targetHubs[0] : targetHubs[1];
        Debug.Log(nearestHub);
        return nearestHub;
    }
    private void travelToHub(int targetedHub)
    {
        findClosedCollumnToTarget(employeePositionRow, EmployeeHubs);
        if (targetHubs[0] != EmployeeHubs[0] && targetHubs[1] != EmployeeHubs[1])
        {
            traveledDistance = traveledDistance + Mathf.Abs(targetedHub - employeePositionCollumn);
            Debug.Log("backtohub");
            employeePositionCollumn = targetedHub;
        }
    }
    private void changeRow()
    {
        if (employeePositionRow != targetPosRow)
        {
            traveledDistance = traveledDistance + Mathf.Abs(targetPosRow - employeePositionRow);
        }
    }
    private void travelToTarget()
    {
        traveledDistance = traveledDistance + Mathf.Abs(targetPosCollumn - employeePositionCollumn);
    }


    
        
            /**
            * permutation function
            * @param str string to
            calculate permutation for
            * @param l starting index
            * @param r end index
            */
            private static void permute(String str,
                                        int l, int r)
            {
                if (l == r)
            Debug.Log(0);
            else
                {
                    for (int i = l; i <= r; i++)
                    {
                        str = swap(str, l, i);
                        permute(str, l + 1, r);
                        str = swap(str, l, i);
                }
                }
            }

            /**
            * Swap Characters at position
            * @param a string value
            * @param i position 1
            * @param j position 2
            * @return swapped string
            */
            public static String swap(String a,
                                    int i, int j)
            {
                char temp;
                char[] charArray = a.ToCharArray();
                temp = charArray[i];
                charArray[i] = charArray[j];
                charArray[j] = temp;
                string s = new string(charArray);
                return s;
            }

            // Driver Code
            public static void Main()
            {
                
            }
        }

        // This code is contributed by mits

    

