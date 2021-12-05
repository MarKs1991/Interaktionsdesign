using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartPosition : MonoBehaviour
{
    public Transform Player;
    public int Distance = 15;
    void Start()
    {
        gameObject.transform.position = new Vector3(Player.position.x, 0, Player.position.z - Distance);
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(Player.position.x, 0, Player.position.z - Distance);
    }
}
