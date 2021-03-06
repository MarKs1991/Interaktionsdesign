using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HololensTrigger : MonoBehaviour
{
    public GameObject Canvas;
    public bool hololensOn = false;
    public GameObject Sphere;
    private bool animStarted = false;
    private bool sphereDestroyed = false;
    private void Start()
    {
        Canvas.SetActive(false);
        Sphere.SetActive(false);
    }

    private void Update()
    {
        if (Sphere != null)
        {
            if (animStarted && !Sphere.GetComponent<Animation>().isPlaying)
            {
                Destroy(Sphere);

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.layer == 8)
        {
            if (other.gameObject != null)
            {
                Canvas.SetActive(true);
                hololensOn = true;
                Destroy(other.gameObject);
                Sphere.SetActive(true);
                Sphere.GetComponent<Animation>().Play();
                animStarted = true;
            }
        }
    }
}
