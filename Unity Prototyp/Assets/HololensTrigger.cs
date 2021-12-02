using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HololensTrigger : MonoBehaviour
{
    public GameObject Canvas;
    public bool hololensOn = false;

    private void Start()
    {
        Canvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Canvas.SetActive(true);
            hololensOn = true;
            Destroy(other.gameObject);
        }
    }
}
