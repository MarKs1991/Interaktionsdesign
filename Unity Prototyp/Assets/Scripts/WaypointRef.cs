using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointRef : MonoBehaviour
{
    public GameObject BinLeft;
    public GameObject BinRight;
    public GameObject RowLeft;
    public GameObject RowRight;
    public int closestNorternHub;
    // Start is called before the first frame update
    void Start()
    {
        BinLeft = RowLeft.transform.GetChild(this.transform.GetSiblingIndex() - 1 - closestNorternHub).gameObject;
        BinRight = RowRight.transform.GetChild(this.transform.GetSiblingIndex() - 1 - closestNorternHub).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
