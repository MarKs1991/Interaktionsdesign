using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderDisplay : MonoBehaviour
{
    public Transform ItemNameUiCollumn;
    public Transform BinIndexUiCollumn;
    public Transform ItemAmountUiCollumn;
    public Transform PickedItemAmountUiCollumn;
    public GameObject OrderItemPrefab;


    public GameObject BinRows;
    //public List<Vector3Int> BinOrderList { get; set; }


    public void generateOrderList(List<Vector3Int> BinOrderList)
    {

        for (int i = 0; i <= BinOrderList.Count - 1; i++)
        {
            BinRef binRef = BinRows.transform.GetChild(BinOrderList[i].x - 1).gameObject.transform.GetChild(BinOrderList[i].y - 1).gameObject.GetComponent<BinRef>();
           
            string ItemName = binRef.BinItem;
            string BinIndex = binRef.BinIndex.ToString();
            string ItemAmount = binRef.ItemAmountinOrder.ToString();

            CreateItemUiElement(ItemNameUiCollumn, ItemName);
            CreateItemUiElement(BinIndexUiCollumn, BinIndex);
            CreateItemUiElement(ItemAmountUiCollumn, ItemAmount, "/ ");
            CreateItemUiElement(PickedItemAmountUiCollumn, 0.ToString());
        }
    }
    private void CreateItemUiElement(Transform Parent, string information)
    {
        GameObject orderItem = Instantiate(OrderItemPrefab, Parent);
        orderItem.GetComponent<TextMeshProUGUI>().text = information;
    }
    private void CreateItemUiElement(Transform Parent, string information, string Addon)
    {
        GameObject orderItem = Instantiate(OrderItemPrefab, Parent);
        orderItem.GetComponent<TextMeshProUGUI>().text = Addon + information;
    }

    public void UpdateBins(BinRef bin)
    {
        
        string pickedUpBinIndex = bin.BinIndex.ToString();
        Debug.Log(pickedUpBinIndex);
        int leftOverAmount = bin.LeftOverAmountinOrder;
        int completeAmount = bin.ItemAmountinOrder;

        for (int i = 0; i <= BinIndexUiCollumn.childCount - 1; i++)
        {

            string checkedbinIndex = BinIndexUiCollumn.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text;
            if (pickedUpBinIndex.Equals(checkedbinIndex))
            {
                PickedItemAmountUiCollumn.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = (completeAmount - leftOverAmount).ToString();
            }
        }
    }
}
