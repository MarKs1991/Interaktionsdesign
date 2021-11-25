using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class UISwipe : MonoBehaviour
{
    private bool isOut = false;
    public string MoveOutAnimation;
    public string MoveInAnimation;

    void Update()
    {
        if (!this.GetComponent<Animation>().isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.O) && !this.isOut)
                PlayAnimation(MoveOutAnimation);
            if (Input.GetKeyDown(KeyCode.I) && this.isOut)
                PlayAnimation(MoveInAnimation);

            //Trigger swipe
         
        }
    }

    void PlayAnimation(string clipName)
    {
        this.GetComponent<Animation>().Play(clipName);
        this.isOut = !this.isOut;
    }
}
