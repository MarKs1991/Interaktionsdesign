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

    public Transform HeadTransform;
    private Vector3 StartingPos;
    public Transform collider;
    public float distance;

    public float sensitivity = .2f;
    public int Direction = 1;
    public Vector3 transformedVector;

    private bool triggerPressed;
    private void Start()
    {
        TriggerPressed.AddOnStateDownListener(SwipeEvent, SteamVR_Input_Sources.Any);

    }

    void Update()
    {
        transformHmdMatrixtoVirtMatrix();
     

        if (!this.GetComponent<Animation>().isPlaying)
        {
            if (triggerPressed)
            {
                
                if ((transformedVector.x - StartingPos.x) * Direction > sensitivity && this.isOut)
                {
                    PlayAnimation(MoveInAnimation);
                    triggerPressed = false;
                    Debug.Log("c");
                }
                else if ((transformedVector.x - StartingPos.x) * Direction < -sensitivity && !this.isOut)
                {
                    PlayAnimation(MoveOutAnimation);
                    triggerPressed = false;
                    Debug.Log("o");
                }
            }
        }
    }

        /*
        if (triggerPressed)
        {
            distance = Vector3.Distance(ControllerPos.position, HeadTransform.position);
            
            collider.transform.position = ControllerPos.transform.position;
            collider.transform.rotation = ControllerPos.transform.rotation;
        }
    }
        */
        /*
        private void OnTriggerExit(Collider other)
        {

            if (this.isOut && distance > Vector3.Distance(ControllerPos.position, HeadTransform.position))
            {
                PlayAnimation(MoveInAnimation);
                triggerPressed = false;
                Debug.Log("c");
            }
            else if (!this.isOut && distance < Vector3.Distance(ControllerPos.position, HeadTransform.position))
            {
                PlayAnimation(MoveOutAnimation);
                triggerPressed = false;
                Debug.Log("o");
            }
        }
        */
        void PlayAnimation(string clipName)
        {
            this.GetComponent<Animation>().Play(clipName);
            this.isOut = !this.isOut;
        }

        void SwipeEvent(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            StartingPos = transformedVector;
            triggerPressed = true;
            Debug.Log("r");
        }

         void transformHmdMatrixtoVirtMatrix()
        {
            HeadTransform.transform.rotation = HeadTransform.rotation * Quaternion.Inverse(HeadTransform.transform.rotation);

            //Vector3 HmdTrackerWorldPos = ControllerPos.transform.TransformPoint(hea.transform.localPosition);
            Vector3 ControllerWorldPos = HeadTransform.transform.TransformPoint(HeadTransform.transform.localPosition);

            Vector3 HmdPosinVirtMatrix = ControllerPos.InverseTransformPoint(ControllerWorldPos);
            transformedVector = HmdPosinVirtMatrix;
            //return transformedVector;
        }
        /*
        private void ComputeTwistAngle()
        {
            // Calculate simple euler angle offset on desired local axis
            var currentInteractorAngleOnAxis = followingInteractor.transform.localEulerAngles[axisOfRotation];
            var interactorAngleDelta = previousInteractorAngleOnAxis - currentInteractorAngleOnAxis;
            // Helper function from Unity Forums to wrap angle in a range
            interactorAngleDelta = Wrap(interactorAngleDelta, maxRotationalDelta, -maxRotationalDelta);
            // Clamp the angle within dial's rotation limits
            targetAngle = Mathf.Clamp(targetAngle + interactorAngleDelta, MinAngle, MaxAngle);
            previousInteractorAngleOnAxis = currentInteractorAngleOnAxis;
        }
        */
}




/*
public class UISwipe : MonoBehaviour
{
    private bool isOut = false;
    public string MoveOutAnimation;
    public string MoveInAnimation;

    public SteamVR_Action_Boolean TriggerPressed;

    public Transform ControllerPos;
    public Transform HeadRot;
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
                Transform test = ControllerPos;
                test.localRotation = HeadRot.localRotation;
                Debug.Log(test.localPosition.x - StartingPos.x);
                if ((test.localPosition.x - StartingPos.x) * Direction > sensitivity && this.isOut)
                {
                    PlayAnimation(MoveInAnimation);
                    triggerPressed = false;
                    Debug.Log("c");
                }
                else if ((test.localPosition.x - StartingPos.x) * Direction < -sensitivity && !this.isOut)
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
        Transform temp = ControllerPos;
        temp.localRotation = HeadRot.localRotation;
        StartingPos = temp.localPosition;
        triggerPressed = true;
        Debug.Log("r");
    }
}*/