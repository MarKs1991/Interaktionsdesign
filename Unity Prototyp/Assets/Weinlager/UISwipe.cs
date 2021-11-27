using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class UISwipe : MonoBehaviour
{
    private bool isOut = false;
    public string MoveOutAnimation;
    public string MoveInAnimation;

    public SteamVR_Action_Boolean TriggerPressed;

    public Transform ControllerPos;
    private Vector3 StartingPos;
    public float sensitivity = .2f;
    public int Direction = 1;

    private bool triggerPressed;
    private void Start()
    {
        TriggerPressed.AddOnStateDownListener(rightSwipeEvent, SteamVR_Input_Sources.Any);

    }

    void Update()
    {
        if (!this.GetComponent<Animation>().isPlaying)
        {
            if (triggerPressed)
            {
                Debug.Log(ControllerPos.localPosition.x - StartingPos.x);
                if ((ControllerPos.localPosition.x - StartingPos.x) * Direction < sensitivity && this.isOut)
                {
                    PlayAnimation(MoveInAnimation);
                    triggerPressed = false;
                    Debug.Log("c");
                }
                else if ((ControllerPos.localPosition.x - StartingPos.x) * Direction > -sensitivity && !this.isOut)
                {
                    PlayAnimation(MoveOutAnimation);
                    triggerPressed = false;
                    Debug.Log("o");
                }
            }
        }
    }
    void PlayAnimation(string clipName)
    {
        this.GetComponent<Animation>().Play(clipName);
        this.isOut = !this.isOut;
    }

    void rightSwipeEvent(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        StartingPos = ControllerPos.localPosition;
        triggerPressed = true;
        Debug.Log("r");
    }
}
