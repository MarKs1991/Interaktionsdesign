using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform Player;

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        gameObject.transform.LookAt(Player);
        gameObject.transform.localEulerAngles = new Vector3(0, gameObject.transform.localEulerAngles.y + 180, 0);
    }
}
