using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPosition : MonoBehaviour
{
    public Transform Player;
    public Transform Headset;

    void Update()
    {
        transform.position = new Vector3(Player.position.x, transform.position.y, Player.position.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Headset.eulerAngles.y, transform.eulerAngles.z);
    }
}
