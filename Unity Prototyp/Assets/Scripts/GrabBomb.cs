using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class GrabBomb : MonoBehaviour
{
    public PlanePilot planePilot;

    public Transform pointer;//the transform the laser starts at
    public LayerMask thingsWeCanGrab;//things we can grab

    public GameObject Grabable;

    Hand hand;//our hand
    bool isAttached = false;//do we have something in our hand?
    GameObject attachedObject = null;//what do we have in our hand
    //Blank blank;//blank script
    public float extraForce = 15;
    public GameObject vrCamera;
    public GameObject Player;
    public SteamVR_Action_Boolean triggerPressRight;
    public SteamVR_Action_Boolean triggerPressLeft;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<Hand>(); //get Right Hand
    }
    // Update is called once per frame
    void Update()
    {
        triggerGrab();
        shootControls();
    }

    private void shootControls()
    {
        bool triggerPressed = triggerPressLeft.GetState(SteamVR_Input_Sources.Any);
        if (triggerPressed)
        {            
            gameObject.GetComponent<RocketFiring>().LaunchRocket();      
        }
    }


    private void LateUpdate()
    {
        //did we get an object to our hand during this update?
        if (isAttached)
        {
            //attach the object
            hand.AttachObject(attachedObject, GrabTypes.Grip);
            //attachedObject = null;
            //isAttached = false;
        }

        bool triggerPressed = triggerPressRight.GetState(SteamVR_Input_Sources.Any);

        if (isAttached && !triggerPressed)
        {
            hand.DetachObject(attachedObject);
            //hand.HoverUnlock(interactable);
            attachedObject.GetComponent<Rigidbody>().useGravity = true;
            calculateThrowingVelocity();
            pointer.position = transform.position;
            attachedObject = null;
            isAttached = false;
        }
    }
    

    private void calculateThrowingVelocity()
    {
        Vector3 velocity;
        Vector3 angularVelocity;

        attachedObject.GetComponent<Throwable>().GetReleaseVelocities(hand, out velocity, out angularVelocity);

        Vector3 handVelocity = hand.GetComponent<SteamVR_Behaviour_Pose>().poseAction.GetVelocity(SteamVR_Input_Sources.RightHand);
        //Debug.Log("handVelocity: " + handVelocity);

        Vector3 handAngularVelocity = hand.GetComponent<SteamVR_Behaviour_Pose>().poseAction.GetAngularVelocity(SteamVR_Input_Sources.RightHand);
        //Debug.Log("handAngularVelocity: " + handAngularVelocity);

        Vector3 GliderVelocity = Player.GetComponent<Rigidbody>().velocity;
        Vector3 angularGliderVelocity = Player.GetComponent<Rigidbody>().angularVelocity;

        attachedObject.GetComponent<Rigidbody>().velocity = (velocity * 5) + GliderVelocity;
        attachedObject.GetComponent<Rigidbody>().angularVelocity = (angularVelocity * 5) + angularGliderVelocity;
        
        //Debug.Log("angularVelocity: " + angularVelocity);
        //Debug.Log("velocity: " + velocity);    
    }

    private void triggerGrab()
    {
        bool triggerPressed = triggerPressRight.GetState(SteamVR_Input_Sources.Any);

        SteamVR_Input_Sources source = hand.handType;


        //are we pressing grip and trigger?
        if (triggerPressed)
        {
            if (!isAttached)
            {
                GameObject GrabableInst = Instantiate(Grabable, hand.transform.position, Quaternion.identity.normalized);
                Interactable interactable = GrabableInst.GetComponent<Interactable>();

                //does the interactable component exist?
                if (interactable != null)
                {
                    //move the object to your hand
                    interactable.transform.LookAt(transform);
                    interactable.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5000000, ForceMode.Impulse);
                    attachedObject = interactable.gameObject;
                    isAttached = true;
                    //attaching to hand is in the late update function
                }
            }
        }
        //blank = hit.collider.gameObject.GetComponentInChildren<Blank>();
        //blank.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
