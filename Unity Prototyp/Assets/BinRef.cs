using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinRef : MonoBehaviour
{
    public GameObject BinLeft;
    public GameObject BinRight;
    public int closestNorternHub;
    public int RowLength;
    public GameObject RowLeft;
    public GameObject RowRight;

    private void Start()
    {
        BinLeft = RowLeft.transform.GetChild(this.transform.GetSiblingIndex() -1 - closestNorternHub).gameObject;
        BinRight = RowRight.transform.GetChild(this.transform.GetSiblingIndex() -1 - closestNorternHub).gameObject;
    }
}
