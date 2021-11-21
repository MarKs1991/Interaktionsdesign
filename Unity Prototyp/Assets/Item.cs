using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector2Int BinIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        BinIndex = transform.parent.gameObject.GetComponent<BinRef>().BinIndex;
        
    }

    // Update is called once per frame
    public void addItemToCart()
    {
        GetComponent<BinRef>().ItemAmountinOrder--;
    }
    public bool checkRequiredAmount()
    {
        if(GetComponent<BinRef>().ItemAmountinOrder > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
