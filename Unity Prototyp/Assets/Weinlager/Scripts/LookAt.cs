using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform Player;

    void Update()
    {
        gameObject.transform.LookAt(Player);
        gameObject.transform.localEulerAngles = new Vector3(0, gameObject.transform.localRotation.x+180, 0);
    }
}
