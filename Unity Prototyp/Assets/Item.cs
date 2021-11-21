using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector2Int BinIndex;
    private BinRef bin;
    
    // Start is called before the first frame update
    void Start()
    {
        bin = transform.parent.gameObject.GetComponent<BinRef>();
        BinIndex = bin.BinIndex;
        
    }

    // Update is called once per frame
    public void addItemToCart()
    {
        bin.ItemAmountinOrder = bin.ItemAmountinOrder - 1;
    }
    public bool checkRequiredAmount()
    {
        if(bin.ItemAmountinOrder > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
