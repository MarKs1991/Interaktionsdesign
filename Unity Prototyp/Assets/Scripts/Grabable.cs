using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Grabable : MonoBehaviour
{
    Interactable interactable;

    void Start()
    {
        interactable = GetComponent<Interactable>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();
    }
    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }
    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        if (interactable.attachedToHand == null || grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType);
            hand.HoverLock(interactable);
            hand.HideGrabHint();

        }
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }
    }
}
