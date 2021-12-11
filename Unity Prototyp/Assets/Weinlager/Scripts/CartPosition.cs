using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartPosition : MonoBehaviour
{
    public Transform Player;
    public int Distance = 10
        ;
    void Start()
    {
        gameObject.transform.position = new Vector3(Player.position.x, 0, Player.position.z - Distance);
    }

    void Update()
    {
        float x = Player.localPosition.x;
        bool paletteOnRight = true;
        if (x > 155)
            paletteOnRight = true;
        else if (x <= 155 && x > 125)
            paletteOnRight = false;
        else if (x <= 125 && x > 95)
            paletteOnRight = true;
        else if (x <= 95 && x > 65)
            paletteOnRight = false;
        else if (x <= 65 && x > 35)
            paletteOnRight = true;
        else if (x <= 35 && x > 5)
            paletteOnRight = false;
        else if (x <= 5 && x > -25)
            paletteOnRight = true;
        else if (x <= -25 && x > -55)
            paletteOnRight = false;
        else if (x <= -55 && x > -85)
            paletteOnRight = true;
        else if (x <= -85 && x > -115)
            paletteOnRight = false;
        else if (x <= -115 && x > -145)
            paletteOnRight = true;
        else if (x <= -145)
            paletteOnRight = false;

        if(paletteOnRight)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 90, 0);
            gameObject.transform.position = new Vector3(Player.position.x + Distance, 0, Player.position.z);
        }
        else
        {
            gameObject.transform.localEulerAngles = new Vector3(0, -90, 0);
            gameObject.transform.position = new Vector3(Player.position.x - Distance, 0, Player.position.z);
        }
    }
}
