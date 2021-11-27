using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public BinRef binRef;

    void Start()
    {
        binRef = transform.parent.gameObject.GetComponent<BinRef>();       
    }   
}
