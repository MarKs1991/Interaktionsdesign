using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateOrder : MonoBehaviour
{
    public Vector3Int[] Order = new Vector3Int[7];
    public BinWaypointTranslater BinWaypointTranslater;
    public TextMeshProUGUI OrderItemNames;
    public TextMeshProUGUI OrderItemAmounts;
    private BinRef[] Bins;
    private BinRef[] OrderBins = new BinRef[7];
    private int[] OrderAmounts = new int[7];
    private int totalAmount = 0;
    
    private List<string> Wines = new List<string>();
    void Start()
    {
        GameObject Hubs = GameObject.Find("Hubs");
        Bins = Hubs.transform.GetComponentsInChildren<BinRef>();
        for (int i = 0; i < 7; i++)
        {
            BinRef bin = getRandomBin();
            int amount = Random.Range(1, 4);
            totalAmount += amount;
            Order[i] = new Vector3Int(bin.BinIndex.x, bin.BinIndex.y, amount);
            OrderBins[i] = bin;
            OrderAmounts[i] = amount;
            
        }

        if (totalAmount > 12)
        {
            reduceTotalAmount();
        }

        for (int j = 0; j < Order.Length; j++)
        {
            OrderItemNames.text += OrderBins[j].BinItem + "\n\n";
            OrderItemAmounts.text += OrderAmounts[j] + "\n\n";
            BinWaypointTranslater.OrderBins.Add(Order[j]);
        }
    }

    private BinRef getRandomBin()
    {
        BinRef bin = Bins[Random.Range(0, Bins.Length)];

        if (!Wines.Contains(bin.BinItem))
        {
            Wines.Add(bin.BinItem);
            return bin;
        }
        else
        {
            return getRandomBin();
        }
    }

    private void reduceTotalAmount()
    {
        int randomInt = Random.Range(0, 7);
        BinRef randomBin = OrderBins[randomInt];
        if(OrderAmounts[randomInt] > 1)
        {
            OrderAmounts[randomInt] -= 1;
            totalAmount -= 1;
            Order[randomInt] = new Vector3Int(Order[randomInt].x, Order[randomInt].y, OrderAmounts[randomInt]);
        }
        else
        {
            reduceTotalAmount();
        }

        if (totalAmount <= 12)
            return;
        else
            reduceTotalAmount();
    }
}
