using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HololensTrigger : MonoBehaviour
{
    public GameObject Canvas;

    private void Start()
    {
        Canvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Canvas.SetActive(true);
            Debug.Log("hit");
            Destroy(other.gameObject);
        }
    }
}
